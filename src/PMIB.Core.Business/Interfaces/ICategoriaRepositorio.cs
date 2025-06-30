using PMIB.Core.Business.Models;

namespace PMIB.Core.Business.Interfaces;

public interface ICategoriaRepositorio : IRepository<Categoria>
{
    Task<Categoria> ObterPorNome(string nome);
}
