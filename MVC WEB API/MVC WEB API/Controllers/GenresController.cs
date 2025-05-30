using Microsoft.AspNetCore.Mvc;
using MVC_WEB_API.Models;

namespace MVC_WEB_API.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly DbContext db;

        public GenresController(DbContext context)
        { db = context; }
           
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            var genres = await db.Genres.Include(x => x.Discs).ToListAsync();
            return genres;
        }
    }
}
