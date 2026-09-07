using EventFlow.Worker;
using EventFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<EventFlowDbContext>(options => 
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("EventFlow")
    )
);

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
