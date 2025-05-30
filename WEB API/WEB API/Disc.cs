
namespace WEB_API.Models;
public class Disc
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public double Price { get; set; }

    public int GenreId { get; set; }
    public Genre? Genre { get; set; } 
}
