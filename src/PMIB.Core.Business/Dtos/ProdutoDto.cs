namespace PMIB.Core.Business.Dtos;

public class ProdutoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public string Descricao { get; set; }
    public int QuantidadeEstoque { get; set; }
    public Guid VendedorId { get; set; }
    public Guid CategoriaId { get; set; }
    public bool Ativo { get; set; }

    //EF Relationships
    public VendedorDto Vendedor { get; set; }
    public CategoriaDto Categoria { get; set; }
}
