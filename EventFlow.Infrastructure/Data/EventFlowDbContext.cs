using Microsoft.EntityFrameworkCore;
using EventFlow.Infrastructure.Models;

namespace EventFlow.Infrastructure.Data;

public class EventFlowDbContext : DbContext
{
    public EventFlowDbContext(DbContextOptions<EventFlowDbContext> options) : base(options){}

    public DbSet<Event> Events => Set<Event>();
}