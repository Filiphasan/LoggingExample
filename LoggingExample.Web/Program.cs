using LoggingExample.Web;
using LoggingExample.Web.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// First way of register serilog
builder.Configuration.RegisterLogger();
builder.Host.UseSerilog();

// Second way of register serilog
// builder.RegisterSerilog();

// Third way of register serilog
// builder.Host.UseSerilog((_, loggerConfig) => loggerConfig.ReadFrom.Configuration(builder.Configuration));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterWeb();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseWeb();

app.UseHealthChecks("/api/health-check");
app.Run();