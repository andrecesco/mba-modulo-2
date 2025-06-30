using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMIB.Core.Business.Models;

namespace PMIB.Core.Data.Mappings;

public class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Email)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.ToTable("Clientes");
    }
}