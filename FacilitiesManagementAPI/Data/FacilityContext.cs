using Microsoft.EntityFrameworkCore;

// Database context for managing facility records
public class FacilityContext : DbContext
{
    public FacilityContext(DbContextOptions<FacilityContext> options) : base(options) { }

    public DbSet<Facility> Facilities { get; set; }
}