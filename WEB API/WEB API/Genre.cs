﻿namespace WEB_API.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Disc> Discs { get; set; } /*= new()*/;
}

