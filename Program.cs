using DataStreamingService;
using DataStreamingService.Data;
using DataStreamingService.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DataRepository>();
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<WebSocketHandler>();
builder.Services.AddSingleton<FaultTolerantService>();
builder.Services.AddSingleton<LoggingService>();

builder.Services.AddHostedService<Worker>();

builder.Services.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));



Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Configuration)
               .CreateLogger();

var host = builder.Build();
host.Run();