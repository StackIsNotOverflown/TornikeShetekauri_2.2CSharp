using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_API.Models;

namespace WEB_API.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController : ControllerBase
{
    private readonly MusicDbContext _context;

    public GenresController(MusicDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        var genres = await _context.Genre.Include(g => g.Discs).ToListAsync();
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        var genre = await _context.Genre.Include(g => g.Discs).FirstOrDefaultAsync(g => g.Id == id);
        if (genre == null)
            return NotFound();

        return Ok(genre);
    }
}

