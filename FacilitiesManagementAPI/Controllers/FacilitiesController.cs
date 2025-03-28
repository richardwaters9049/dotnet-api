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