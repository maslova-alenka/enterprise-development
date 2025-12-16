var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql");
var mysqlDb = mysql.AddDatabase("mysqldb");

var api = builder.AddProject<Projects.Polyclinic_Api>("polyclinic-api")
    .WithReference(mysqlDb, "mysqldb")
    .WaitFor(mysqlDb);

builder.AddProject<Projects.Polyclinic_Grpc_Client>("polyclinic-grpc-client")
    .WithEnvironment("Worker__ServerAddress", api.GetEndpoint("https"))
    .WaitFor(api);

builder.Build().Run();