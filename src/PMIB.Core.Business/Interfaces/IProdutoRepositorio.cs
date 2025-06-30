using PMIB.Core.Business.Models;

namespace PMIB.Core.Business.Interfaces;

public interface IProdutoRepositorio : IRepository<Produto>
{
    Task<List<Produto>> ObterPorVendedorId(Guid vendedorId);
    Task<List<Produto>> ObterPorCategoriaId(Guid categoriaId);
}
