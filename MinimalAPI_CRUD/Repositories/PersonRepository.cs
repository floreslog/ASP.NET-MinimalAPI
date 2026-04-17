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
            using (var conexion = new SqlConnection(connectionString))
            {
                var persons = await conexion.QueryAsync<Person>("SP_GetAllPersons", 
                    commandType: CommandType.StoredProcedure);
                return persons;
            }
        }

        public async Task<Person?> GetById(int IDPerson)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var person = await conexion
                    .QueryFirstOrDefaultAsync<Person>("SP_GetPersonById",
                    new {IDPerson}, commandType: CommandType.StoredProcedure);

                return person;
            }
        }

        public async Task<int> Create(Person person)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                var id = await conexion.QuerySingleAsync<int>("SP_InsertPerson",
                    new {person.Name, person.Last_name, person.CURP},
                    commandType: CommandType.StoredProcedure);

                person.IDPerson = id;
                return id;
            }
        }









    }
}
