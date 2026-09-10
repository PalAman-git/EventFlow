using Microsoft.EntityFrameworkCore;
using EventFlow.Infrastructure.Models;

namespace EventFlow.Infrastructure.Data;

public class EventFlowDbContext : DbContext
{
    public EventFlowDbContext(DbContextOptions<EventFlowDbContext> options) : base(options){}

    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
            .Property(e => e.Payload)
            .HasColumnType("jsonb");
    }
}