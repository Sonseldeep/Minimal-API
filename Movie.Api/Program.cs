using Movie.Api.Database;
using Movie.Api.Endpoints;
using Movie.Api.Extensions;
using Movie.Api.Middleware;
using Movie.Api.Repositories.Implementations;
using Movie.Api.Repositories.Interface;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
    };
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MovieDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});




builder.Services.AddScoped<IMovieRepository, MovieRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.ApplyMigrationsAsync();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();


app.MapMovieEndpoints();

await app.RunAsync();