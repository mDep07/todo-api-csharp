using Microsoft.EntityFrameworkCore;
using TodoAPI.Configurations;
using TodoAPI.Models;

namespace TodoAPI.Data;

class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }
    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TodoConfiguration());
    }
}