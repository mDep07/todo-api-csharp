namespace TodoAPI.Models;

class Entity
{
    public int Id { get; set; }
    public DateTimeOffset Created { get; set; } = DateTime.Now;
    public DateTimeOffset? Updated { get; set; }
    public DateTimeOffset? Deleted { get; set; }
}