using Microsoft.EntityFrameworkCore;
using EventFlow.Models;

namespace EventFlow.Data;

public class EventFlowDbContext : DbContext
{
    public EventFlowDbContext(DbContextOptions<EventFlowDbContext> options) : base(options)
    {
        
    }

    public DbSet<Event> Events => Set<Event>();
}