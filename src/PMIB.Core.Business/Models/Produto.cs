namespace PMIB.Core.Business.Models;

public class Produto : Entity
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeEstoque { get; set; }
    public Guid VendedorId { get; set; }
    public Guid CategoriaId { get; set; }
    public bool Ativo { get; set; }

    //EF Relationships
    public Vendedor Vendedor { get; set; }
    public Categoria Categoria { get; set; }
}
