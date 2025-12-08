var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql");
var mysqlDb = mysql.AddDatabase("mysqldb");

builder.AddProject<Projects.Polyclinic_Api>("polyclinic-api")
    .WithReference(mysqlDb, "mysqldb")
    .WaitFor(mysqlDb);

builder.Build().Run();