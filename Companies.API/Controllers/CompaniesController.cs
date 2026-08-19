using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Companies.API.Models.Entities;
using Companies.API.Data;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CompaniesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Company
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
    {
        var dto =  await _context.Companies.Include(c => c.Address).ToListAsync();

        return dto;
    }

    // GET: api/Company/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompany(Guid id)
    {
        var company = await _context.Companies.FindAsync(id);

        if (company == null)
        {
            return NotFound();
        }

        return company;
    }

    //// PUT: api/Company/5
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutCompany(System.Guid? id, Company company)
    //{
    //    if (id != company.Id)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(company).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!CompanyExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}

    //// POST: api/Company
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPost]
    //public async Task<ActionResult<Company>> PostCompany(Company company)
    //{
    //    _context.Companies.Add(company);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction("GetCompany", new { id = company.Id }, company);
    //}

    //// DELETE: api/Company/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteCompany(System.Guid? id)
    //{
    //    var company = await _context.Companies.FindAsync(id);
    //    if (company == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Companies.Remove(company);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool CompanyExists(System.Guid? id)
    //{
    //    return _context.Companies.Any(e => e.Id == id);
    //}
}
