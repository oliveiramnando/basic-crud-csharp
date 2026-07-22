using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.ToTable("TodoItems");

            entity.HasKey(todo => todo.Id);

            entity.Property(todo => todo.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(todo => todo.Description)
                .HasMaxLength(1000);

            entity.Property(todo => todo.IsCompleted)
                .HasDefaultValue(false);

            entity.Property(todo => todo.CreatedAtUtc)
                .IsRequired();
        });
    }
}
