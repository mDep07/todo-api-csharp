using TodoAPI.Models;

namespace TodoAPI.DataContracts;

class TodoDC : EntityDC
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset? Completed { get; set; }
    public DateTimeOffset? Deleted { get; set; }

    public TodoDC() {}
    public TodoDC(Todo todo) =>
        (Id, Completed, Deleted, Title, Description) = (todo.Id, todo.Completed, todo.Deleted, todo.Title, todo.Description);
}
