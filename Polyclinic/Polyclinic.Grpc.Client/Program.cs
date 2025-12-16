using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polyclinic.Grpc.Client;
using Polyclinic.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);


builder.AddServiceDefaults();


builder.Services.Configure<WorkerOptions>(
    builder.Configuration.GetSection(WorkerOptions.SectionName));


builder.Services.AddSingleton<AppointmentContractGenerator>();


builder.Services.AddHostedService<Worker>();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Polyclinic Generator starting...");

app.Run();

logger.LogInformation("Polyclinic Generator stopped.");