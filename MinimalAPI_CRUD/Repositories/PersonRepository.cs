using Dapper;
using Microsoft.Data.SqlClient;
using MinimalAPI_CRUD.Entities;
using System;
using System.Data;

namespace MinimalAPI_CRUD.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public readonly string? connectionString;
        public PersonRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var persons = await connection.QueryAsync<Person>("SP_GetAllPersons", 
                    commandType: CommandType.StoredProcedure);
                return persons;
            }
        }

        public async Task<Person?> GetById(int IDPerson)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var person = await connection
                    .QueryFirstOrDefaultAsync<Person>("SP_GetPersonById",
                    new {IDPerson}, commandType: CommandType.StoredProcedure);

                return person;
            }
        }

        public async Task<int> Create(Person person)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var id = await connection.QuerySingleAsync<int>("SP_InsertPerson",
                        new { person.Name, person.Last_name, person.CURP },
                        commandType: CommandType.StoredProcedure);

                    person.IDPerson = id;
                    return id;
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                //curp duplicado
                throw new Exception("CURP already exists");
            }
        }


        public async Task Update(Person person)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync("SP_UpdatePerson",
                    new { person.IDPerson, person.Name },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> ExistsById(int IDPerson)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var exists = await connection.QuerySingleAsync<bool>("SP_ExistsById",
                    new { IDPerson }, commandType: CommandType.StoredProcedure);

                return exists;
            }
        }

        public async Task Delete(int IDPerson)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync("SP_DeletePerson",
                    new { IDPerson }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
