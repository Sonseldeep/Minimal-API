// using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi;
using Movie.Api.Database;
using Movie.Api.Endpoints;
using Movie.Api.Extensions;
using Movie.Api.ImageUploader;
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


builder.Services.AddScoped<IImageUploader, ImageUploader>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MovieDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddAntiforgery();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddSingleton<IImageUploader, ImageUploader>();




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
   
    app.MapOpenApi();
    app.MapScalarApiReference();
    
    await app.ApplyMigrationsAsync();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();


app.MapMovieEndpoints();
app.MapImageEndpoints();


await app.RunAsync();