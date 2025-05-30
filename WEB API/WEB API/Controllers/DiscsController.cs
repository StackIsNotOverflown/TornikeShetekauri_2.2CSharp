using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.Models;

namespace WEB_API.Controllers;

[ApiController]
[Route("api/discs")]
public class DiscsController : ControllerBase
{
    private readonly MusicDbContext _context;

    public DiscsController(MusicDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Disc>>> GetDiscs()
    {
        var discs = await _context.Discs.Include(d => d.Genre).ToListAsync();
        return Ok(discs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Disc>> GetDisc(int id)
    {
        var disc = await _context.Discs.Include(d => d.Genre).FirstOrDefaultAsync(d => d.Id == id);
        if (disc == null)
            return NotFound();

        return Ok(disc);
    }
}
