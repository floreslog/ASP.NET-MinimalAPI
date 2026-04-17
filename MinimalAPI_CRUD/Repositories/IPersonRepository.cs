using MinimalAPI_CRUD.Entities;

namespace MinimalAPI_CRUD.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
        Task<int> Create(Person person);
    }
}