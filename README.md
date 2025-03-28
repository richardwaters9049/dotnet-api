# Facilities Management API

## Project Overview

This project is a **.NET 9 Web API** designed for managing **facilities, assets, and maintenance tasks**. It aligns with Concerto/Bellrock’s focus on **property and estate management solutions** by providing a structured way to track facilities and related data. The API will allow users to **create, retrieve, and manage facilities data** using a PostgreSQL database.

## Project Aim

- Build a **scalable and modular** web API.
- Use **Entity Framework Core** with **PostgreSQL**.
- Follow **RESTful API principles**.
- Provide clear documentation and structure.

## Expected Output

A working API that allows users to:

1. **Add new facilities** to the database.
2. **Retrieve a list of facilities**.
3. **Store and manage facility details** securely.

---

## **Step 1: Setting Up the Project**

### **Create the Project Directory and Initialize a .NET API**

```sh
mkdir FacilitiesManagementAPI
cd FacilitiesManagementAPI
dotnet new webapi -n FacilitiesManagementAPI
cd FacilitiesManagementAPI
```

### **Install Required Packages**

```sh
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Npgsql
dotnet add package Swashbuckle.AspNetCore
```

### **Create the Database** (Using pgAdmin4 or PSQL CLI)

```sh
PGUSER=postgres PGHOST=localhost PGPASSWORD=*** PGPORT=*** psql -c "CREATE DATABASE facilities_db;"
```

---

## **Project Structure**

```
FacilitiesManagementAPI/
├── Controllers/
│   ├── FacilitiesController.cs
├── Models/
│   ├── Facility.cs
├── Data/
│   ├── FacilityContext.cs
├── Program.cs
├── appsettings.json
```

---

## **Code Implementation**

### **Program.cs (Entry Point)**

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configure PostgreSQL Database Connection
builder.Services.AddDbContext<FacilityContext>(options =>
    options.UseNpgsql("Host=localhost;Database=facilities_db;Username=postgres;Password=riv;Port=5433"));

// Add controllers and Swagger for API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
```

---

### **Data/FacilityContext.cs (Database Context)**

```csharp
using Microsoft.EntityFrameworkCore;

// Database context for managing facility records
public class FacilityContext : DbContext
{
    public FacilityContext(DbContextOptions<FacilityContext> options) : base(options) { }

    public DbSet<Facility> Facilities { get; set; }
}
```

---

### **Models/Facility.cs (Facility Model)**

```csharp
// Defines the structure of a Facility record
public class Facility
{
    public int Id { get; set; }  // Unique identifier
    public string Name { get; set; }  // Name of the facility
    public string Location { get; set; }  // Facility location
}
```

---

### **Controllers/FacilitiesController.cs (API Controller)**

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/facilities")]
public class FacilitiesController : ControllerBase
{
    private readonly FacilityContext _context;

    public FacilitiesController(FacilityContext context)
    {
        _context = context;
    }

    // GET: Retrieve all facilities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Facility>>> GetFacilities()
    {
        return await _context.Facilities.ToListAsync();
    }

    // POST: Add a new facility
    [HttpPost]
    public async Task<ActionResult<Facility>> CreateFacility(Facility facility)
    {
        _context.Facilities.Add(facility);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetFacilities), new { id = facility.Id }, facility);
    }
}
```

---

## **Running the Application**

1. **Apply Migrations and Update the Database**
   ```sh
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
2. **Run the Application**
   ```sh
   dotnet run
   ```
3. **Access Swagger API Documentation**
   Open: `http://localhost:5000/swagger`

---

This project simulates real-world **facilities management** processes using a **scalable .NET architecture**.
