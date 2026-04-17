using Microsoft.AspNetCore.Http.HttpResults;
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

app.MapGet("/persons/{id:int}", async Task<Results<NotFound, Ok<Person>>> (int id, IPersonRepository personRepository) => {
    var person = await personRepository.GetById(id);

    if (person == null)
        return TypedResults.NotFound();

    return TypedResults.Ok(person);
});

app.MapPost("/persons",
    async Task<Results<Created<Person>, BadRequest<string>, ProblemHttpResult>>
    (Person person, IPersonRepository personRepository) =>
    {
        try
        {
            await personRepository.Create(person);
            return TypedResults.Created($"/persons/{person.IDPerson}", person);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("CURP"))
                return TypedResults.BadRequest("CURP already exists");

            return TypedResults.Problem("Unexpected error");
        }
    });

app.MapPut("/persons/{id:int}", async Task<Results<BadRequest<string>, NoContent, NotFound>> (int id, Person person, IPersonRepository personRepository) =>
{
    if (id != person.IDPerson)
        return TypedResults.BadRequest("El id de la URL no coincide con el id enviado");

    var exists = await personRepository.ExistsById(id);

    if (!exists)
    {
        return TypedResults.NotFound();
    }

    await personRepository.Update(person);
    return TypedResults.NoContent();
});

app.MapDelete("/persons/{id:int}", async
    Task<Results<NoContent, NotFound>>
    (int id, IPersonRepository personRepository) =>
{
    var exists = await personRepository.ExistsById(id);

    if (!exists)
        return TypedResults.NotFound();

    await personRepository.Delete(id);

    return TypedResults.NoContent();
});

app.Run();


