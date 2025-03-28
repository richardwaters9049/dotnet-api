using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetEnv; // Load environment variables

var builder = WebApplication.CreateBuilder(args);

// Load .env file variables
Env.Load();

// Construct the PostgreSQL connection string from .env variables
var connectionString = $"Host={Env.GetString("PGHOST")};Database={Env.GetString("PGDATABASE")};" +
                       $"Username={Env.GetString("PGUSER")};Password={Env.GetString("PGPASSWORD")};Port={Env.GetString("PGPORT")}";

// Configure the database context
builder.Services.AddDbContext<FacilityContext>(options =>
    options.UseNpgsql(connectionString));

// Add controllers for handling API requests
builder.Services.AddControllers();

// Enable API documentation with Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS if needed (adjust policy as necessary)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Enable Swagger in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use HTTPS redirection for security
app.UseHttpsRedirection();

// Apply CORS policy
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

// Map controller endpoints
app.MapControllers();

// Start the application
app.Run();
