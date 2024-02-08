using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetrolPrice.Areas.Identity.Data;
using PetrolPrice.Models;
using System.Reflection.Emit;

namespace PetrolPrice.Data;

public class PetrolPriceContext : IdentityDbContext<PetrolPriceUser>
{
    public PetrolPriceContext(DbContextOptions<PetrolPriceContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<PetrolStation>().ToTable("PetrolStation");
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<PetrolPriceUser>
            (b => { b.HasMany(p => p.PetrolStation); });
        // Configure Price column to use decimal(18,1) in SQL Server
        builder.Entity<PetrolStation>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,1)");
    }

    public DbSet<PetrolStation> PetrolStation { get; set; }
}
