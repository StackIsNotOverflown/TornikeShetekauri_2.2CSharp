using Practice._3.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

public class MachineDbContext : System.Data.Entity.DbContext
{
    public MachineDbContext() : base("name=DefaultConnection")
    {
    }

    public System.Data.Entity.DbSet<Order> Orders { get; set; }
    public System.Data.Entity.DbSet<Customer> Personalis { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
       
        base.OnModelCreating(modelBuilder);
    }
}