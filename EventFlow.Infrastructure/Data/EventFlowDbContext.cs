using Microsoft.EntityFrameworkCore;
using EventFlow.Infrastructure.Models;

namespace EventFlow.Infrastructure.Data;

public class EventFlowDbContext : DbContext
{
    public EventFlowDbContext(DbContextOptions<EventFlowDbContext> options) : base(options){}

    public DbSet<Event> Events => Set<Event>();

    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    public DbSet<EventDelivery> EventDeliveries => Set<EventDelivery>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>()
            .Property(e => e.Payload)
            .HasColumnType("jsonb");
    }
}