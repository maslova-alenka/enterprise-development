var builder = DistributedApplication.CreateBuilder(args);

// Добавляем MySQL
var mysql = builder.AddMySql("mysql")
    .WithDataVolume()
    .AddDatabase("PolyclinicDb");

// Добавляем API проект
var api = builder.AddProject<Projects.Polyclinic_Api>("polyclinic-api")
    .WithReference(mysql);

builder.Build().Run();