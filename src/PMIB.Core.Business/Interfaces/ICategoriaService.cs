using PMIB.Core.Business.Models;
using PMIB.Core.Business.Requests;

namespace PMIB.Core.Business.Interfaces;

public interface ICategoriaService
{
    Task<Categoria> ObterPorId(Guid id);
    Task<IEnumerable<Categoria>> ObterTodos();
    Task<Categoria> Adicionar(CategoriaRequest request);
    Task<Categoria> Atualizar(Guid id, CategoriaRequest request);
    Task<bool> Remover(Guid id);
}
