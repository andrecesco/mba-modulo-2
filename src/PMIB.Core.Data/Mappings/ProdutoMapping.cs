using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMIB.Core.Business.Models;

namespace PMIB.Core.Data.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Descricao)
            .HasColumnType("varchar(300)");

        builder.Property(p => p.Preco)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.ToTable("Produtos");
    }
}
