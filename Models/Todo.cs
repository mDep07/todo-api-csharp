namespace TodoAPI.Models;

class Todo : Entity
{
    public int? ParentId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset? Completed { get; set; }
    public DateTimeOffset? Started { get; set; }
    public ICollection<Todo>? Childs { get; set; }
}