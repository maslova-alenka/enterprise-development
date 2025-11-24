using AutoMapper;
using Polyclinic.Application;
using Polyclinic.Application.Service;
using Polyclinic.Contracts;
using Polyclinic.Contracts.Appointments;
using Polyclinic.Contracts.Doctors;
using Polyclinic.Contracts.Patients;
using Polyclinic.Contracts.Specializations;
using Polyclinic.Domain.Models;
using Polyclinic.Domain.Interfaces;
using Polyclinic.Repositories.InMemory;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(
    config => config.AddProfile(new PolyclinicProfile()),
    LoggerFactory.Create(builder => builder.AddConsole()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSingleton<IRepository<Patient, int>, PatientInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Doctor, int>, DoctorInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Appointment, int>, AppointmentInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Specialization, int>, SpecializationInMemoryRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var assembly = Assembly.GetExecutingAssembly();
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml"));
    foreach (var refAssembly in assembly.GetReferencedAssemblies())
    {
        if (refAssembly.Name!.StartsWith("System.") || refAssembly.Name.StartsWith("Microsoft."))
            continue;

        var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{refAssembly.Name}.xml");
        if (File.Exists(xmlPath))
            options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();