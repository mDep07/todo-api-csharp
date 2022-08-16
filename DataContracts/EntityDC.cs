using TodoAPI.Models;

namespace TodoAPI.DataContracts;

class EntityDC
{
    public int Id { get; set; }
    public DateTimeOffset Created { get; set; } = DateTime.Now;
    public DateTimeOffset? Deleted { get; set; }

    public EntityDC() {}
}
