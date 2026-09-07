using Microsoft.EntityFrameworkCore;
using EventFlow.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<EventFlowDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("EventFlow")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
