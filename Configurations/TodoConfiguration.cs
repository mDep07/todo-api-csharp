using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoAPI.Models;

namespace TodoAPI.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    void IEntityTypeConfiguration<Todo>.Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(20);
        builder.Property(x => x.Description).HasMaxLength(200);
    }
}