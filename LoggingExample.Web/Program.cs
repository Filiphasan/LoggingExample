using Elastic.Apm.NetCoreAll;
using LoggingExample.Web.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// First way of register serilog
// builder.Configuration.RegisterLogger();
// builder.Host.UseSerilog();

// Second way of register serilog
// builder.RegisterSerilog();

// Third way of register serilog
builder.Host.UseSerilog((_, loggerConfig) => loggerConfig
    .ReadFrom.Configuration(builder.Configuration));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<RequestResponseLogMiddleware>();
builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseAllElasticApm(builder.Configuration);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseMiddleware<RequestResponseLogMiddleware>();

app.UseHealthChecks("/api/health-check");
app.Run();