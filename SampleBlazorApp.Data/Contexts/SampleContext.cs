using Microsoft.EntityFrameworkCore;
using SampleBlazorApp.Data.Entities;

namespace SampleBlazorApp.Data.Contexts;

public class SampleContext : DbContext
{
    public SampleContext(DbContextOptions<SampleContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer($"Name={nameof(SampleContext)}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(x => x.City)
                .HasDefaultValue(null)
                .IsRequired(false);

            entity.Property(x => x.State)
                .HasDefaultValue(null)
                .IsRequired(false);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(x => x.Code)
                .HasDefaultValue(null)
                .IsRequired(false);
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<MarketingCategory> MarketingCategories { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<SalesPerson> SalesPeople { get; set; }
}
