using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.DataContracts;
using TodoAPI.Models;

var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseMySQL(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("TodoDB"));

var app = builder.Build();

var options = new JsonSerializerOptions(JsonSerializerDefaults.General);

// app.MapGet("/", () => Results.File());

app.MapGet("/todo", async (TodoDbContext db) => await db.Todos.Select(t => new TodoDC(t)).ToListAsync());

// app.MapGet("/todoitems", async (TodoDbContext db) => await db.Todos.Select(t => new TodoDC(t)).ToListAsync());

// app.MapGet("/todoitems/complete", async (TodoDbContext db) => await db.Todos.Where(t => t.IsComplete).ToListAsync());

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

app.Run();


