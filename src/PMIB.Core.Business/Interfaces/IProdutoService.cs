using PMIB.Core.Business.Models;
using PMIB.Core.Business.Requests;

namespace PMIB.Core.Business.Interfaces;

public interface IProdutoService
{
    Task<Produto> ObterPorId(Guid id);
    Task<IEnumerable<Produto>> ObterTodos();
    Task<Produto> Adicionar(ProdutoRequest request);
    Task<Produto> Atualizar(Guid id, ProdutoRequest request);
    Task<bool> Remover(Guid id);
    Task<IEnumerable<Produto>> ObterPorCategoria(Guid categoriaId);
    Task<IEnumerable<Produto>> ObterPorVendedor(Guid vendedorId);
}
