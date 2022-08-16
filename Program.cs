using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.DataContracts;
using TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("TodoDB"));

var app = builder.Build();

app.MapGet("/todo", async (TodoDbContext db) =>
{
    return await db.Todos.Select(t => new TodoDC(t)).ToListAsync();
});

app.MapGet("/todo/{id}", async (int id, TodoDbContext db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(new TodoDC(todo))
            : Results.NotFound());

app.MapPost("/todo", async (TodoDC todo, TodoDbContext db) =>
{
    var newTodo = new Todo
    {
        Title = todo.Title
    };
    db.Todos.Add(newTodo);
    await db.SaveChangesAsync();

    return Results.Created($"/todo/{newTodo.Id}", new TodoDC(newTodo));
});

app.MapPut("/todo/{id}", async (int id, TodoDC todo, TodoDbContext db) =>
{
    var todoUpdate = await db.Todos.FindAsync(id);
    if(todoUpdate is null) return Results.NotFound();

    todoUpdate.Title = todo.Title;
    todoUpdate.Updated = DateTime.Now;

    await db.SaveChangesAsync();

    return Results.Ok();
});

app.MapDelete("/todo/{id}", async (int id, TodoDbContext db) =>
{
    var todoUpdate = await db.Todos.FindAsync(id);
    if(todoUpdate is null) return Results.NotFound();

    todoUpdate.Deleted = DateTime.Now;

    await db.SaveChangesAsync();

    return Results.Ok();
});

app.MapPut("/todo/{id}/complete", async (int id, TodoDbContext db) =>
{
    var todoUpdate = await db.Todos.FindAsync(id);
    if(todoUpdate is null) return Results.NotFound();

    if(todoUpdate.Deleted != null) return Results.Conflict("This todo is deleted");

    todoUpdate.Updated = DateTime.Now;
    todoUpdate.Completed = DateTime.Now;

    await db.SaveChangesAsync();

    return Results.Ok();
});

app.MapPut("/todo/{id}/start", async (int id, TodoDbContext db) =>
{
    var todoUpdate = await db.Todos.FindAsync(id);
    if(todoUpdate is null) return Results.NotFound();

    if(todoUpdate.Deleted != null) return Results.Conflict("This todo is deleted");
    if(todoUpdate.Completed != null) return Results.Conflict("This todo is completed");

    todoUpdate.Updated = DateTime.Now;
    todoUpdate.Started = DateTime.Now;

    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();


class Query
{
    public bool? Completed { get; set; }
}