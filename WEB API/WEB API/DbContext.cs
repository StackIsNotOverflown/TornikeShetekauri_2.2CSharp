using Microsoft.EntityFrameworkCore;
using WEB_API.Models;


public class MusicDbContext : DbContext
{
    public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
    {
    }

    public DbSet<Genre> Genre { get; set; }
    public DbSet<Disc> Discs { get; set; }
}
   

