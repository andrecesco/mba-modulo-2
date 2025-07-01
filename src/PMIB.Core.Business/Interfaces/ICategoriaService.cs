using PMIB.Core.Business.Dtos;
using PMIB.Core.Business.Models;
using PMIB.Core.Business.Requests;

namespace PMIB.Core.Business.Interfaces;

public interface ICategoriaService
{
    Task<CategoriaDto> ObterPorId(Guid id);
    Task<IEnumerable<CategoriaDto>> ObterTodos();
    Task<Categoria> Adicionar(CategoriaRequest request);
    Task<Categoria> Atualizar(Guid id, CategoriaRequest request);
    Task<bool> Remover(Guid id);
}
