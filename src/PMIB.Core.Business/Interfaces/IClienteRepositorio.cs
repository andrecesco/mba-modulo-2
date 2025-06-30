using PMIB.Core.Business.Models;

namespace PMIB.Core.Business.Interfaces;

public interface IClienteRepositorio : IRepository<Cliente>
{
    Task<Cliente> ObterPorNome(string nome);
}