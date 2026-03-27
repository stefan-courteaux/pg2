using MigrationsPetDemo.Model;

namespace MigrationsPetDemo.Storage;

using Microsoft.EntityFrameworkCore;

public class VetDbContext : DbContext
{
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Horse> Horses { get; set; }
    public DbSet<Hedgehog> Hedgehogs { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Vet> Vets { get; set; }

    // In een volwaardige web api zal je
    // - de context aanmaken voor DI,
    // - de connectionstring uit config lezen.
    // Zie "StoreFront" voorbeeldcode.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(
            "Server=localhost;Port=5432;Userid=admin;Password=secret;Database=pg2db",
            builder => builder.MigrationsHistoryTable(
                "__EFMigrationsHistory","Pets")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Pets");
    }
}