using MinimalAPI_CRUD.Entities;

namespace MinimalAPI_CRUD.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
        Task<int> Create(Person person);
        Task<Person?> GetById(int id);
        Task Update(Person person);
        Task<bool> ExistsById(int IDPerson);
    }
}