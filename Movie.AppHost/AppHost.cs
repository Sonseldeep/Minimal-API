using Projects;

var builder = DistributedApplication.CreateBuilder(args);


var sqlServer = builder.AddSqlServer("sqlserver")
    .WithDataVolume()
    .WithHostPort(1433);

sqlServer.AddDatabase("Database");

builder.AddProject<Movie_Api>("movie-api")
    .WithReference(sqlServer);  

builder.Build().Run();