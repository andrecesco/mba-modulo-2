using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMIB.Core.Business.Models;

namespace PMIB.Core.Data.Mappings;
public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Descricao)
            .HasColumnType("varchar(300)");

        builder.ToTable("Categorias");
    }
}
