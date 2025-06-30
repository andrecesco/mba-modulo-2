using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMIB.Core.Business.Models;

namespace PMIB.Core.Data.Mappings;

public class VendedorMapping : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Email)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.ToTable("Vendedores");
    }
}
