using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMIB.Core.Business.Models;

namespace PMIB.Core.Data.Context;

public class PmibContext : IdentityDbContext
{
    public PmibContext() { }

    public PmibContext(DbContextOptions<PmibContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes().SelectMany(
            e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        builder.ApplyConfigurationsFromAssembly(typeof(PmibContext).Assembly);

        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(builder);
    }
}