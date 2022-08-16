using TodoAPI.Models;

namespace TodoAPI.DataContracts;

class TodoDC : EntityDC
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset? Completed { get; set; }
    public DateTimeOffset? Started { get; set; }
    public TimeSpan? Duration
    {
        get => Started != null && Completed != null ? Completed - Started : null;
    }

    public TodoDC() {}
    public TodoDC(Todo todo)
    {
        Id = todo.Id;
        Completed = todo.Completed;
        Started = todo.Started;
        Deleted = todo.Deleted;
        Title = todo.Title;
        Description = todo.Description;
    }
}
