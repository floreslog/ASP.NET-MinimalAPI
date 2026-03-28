using MinimalAPI_CRUD.Repositories;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddTransient<IPersonRepository, PersonRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/persons", async (IPersonRepository personRepository) =>
{
    var persons = await personRepository.GetAll();
    return persons;
});






app.Run();


