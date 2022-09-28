#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace BetaLottery.Models;
public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    public DbSet<PromoterUser> PromoterUsers { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Participate> Participates { get; set; }
}
