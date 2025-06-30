using Microsoft.EntityFrameworkCore;
using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using PMIB.Core.Data.Context;

namespace PMIB.Core.Data.Repositorios;

public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
{
    public ClienteRepositorio(PmibContext context) : base(context) { }

    public Task<Cliente> ObterPorNome(string nome)
    {
        return Db.Clientes.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nome == nome);
    }
}
