
using Microsoft.EntityFrameworkCore;
using MVC_WEB_API.Models;
using System.Collections.Generic;
using System.Reflection.Emit;


public class DbContext : DbContext
{
    public DbContext(DbContextOptions<DbContext> options) : base(options) { }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Disc> Discs { get; set; }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "როკი" },
        new Genre { Id = 2, Name = "ჯაზი" }
        )

        model.Entity<Disc>().HasData(
            new Disc { Id = 1, Title = "Dark Side", Artist = "Pink Floyd", Price = 29.99, GenreId = 1 },
            new Disc { Id = 2, Title = "Kind of Blue", Artist = "Miles Davis", Price = 19.99, GenreId = 2 },
            new Disc { Id = 3, Title = "Abbey Road", Artist = "The Beatles", Price = 24.50, GenreId = 1 }
        )
    }
}
