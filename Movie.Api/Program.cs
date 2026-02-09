// using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi;
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


// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.MetadataAddress = builder.Configuration["Keycloak:MetadataAddress"]!;
//         options.Audience = builder.Configuration["Keycloak:Audience"];
//
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidIssuer = builder.Configuration["Keycloak:Issuer"]
//         };
//
//         options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
//     });

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();


// var keycloakAuthority = builder.Configuration["Keycloak:Authority"]!;
// var keycloakClientId = builder.Configuration["Keycloak:ClientId"]!;

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(options =>
// {
//     options.SwaggerDoc("v1", new OpenApiInfo
//     {
//         Title = "Public Api",
//         Version = "v1"
//     });
//
//     // Define the OAuth 2.0 security scheme
//     options.AddSecurityDefinition(nameof(SecuritySchemeType.OAuth2), new OpenApiSecurityScheme
//     {
//         Type = SecuritySchemeType.OAuth2,
//         Flows = new OpenApiOAuthFlows
//         {
//             AuthorizationCode = new OpenApiOAuthFlow
//             {
//                 AuthorizationUrl = new Uri($"{keycloakAuthority}/protocol/openid-connect/auth"),
//                 TokenUrl = new Uri($"{keycloakAuthority}/protocol/openid-connect/token"),
//                 Scopes = new Dictionary<string, string>
//                 {
//                     { "openid", "OpenID Connect scope" },
//                     { "profile", "User profile" }
//                 }
//             }
//         }
//     });
//
//     // Apply security to all operations
//     options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
//     {
//         {
//             new OpenApiSecuritySchemeReference(nameof(SecuritySchemeType.OAuth2), doc),
//             []
//         }
//     });
// });

builder.Services.AddScoped<IMovieRepository, MovieRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
    // app.UseSwagger();
    // app.UseSwaggerUI(options =>
    // {
    //     options.OAuthClientId(keycloakClientId);
    //     options.OAuthUsePkce(); 
    // });
    await app.ApplyMigrationsAsync();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapMovieEndpoints();

await app.RunAsync();