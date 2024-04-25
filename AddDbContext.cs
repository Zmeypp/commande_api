using CommandesAPI;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Commande> Commandes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
