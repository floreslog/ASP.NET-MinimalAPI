using MinimalAPI_CRUD.Entities;
using MinimalAPI_CRUD.Repositories;
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

app.MapGet("/persons/{id:int}", async (int id, IPersonRepository personRepository) => { 

});



app.MapPost("/persons", async (Person person, IPersonRepository personRepository) =>
{
    await personRepository.Create(person);
    return TypedResults.Created($"/persons/{person.IDPerson}", person);
});


app.Run();


