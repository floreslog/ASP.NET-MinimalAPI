using Dapper;
using Microsoft.Data.SqlClient;
using MinimalAPI_CRUD.Entities;
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
    }
}
