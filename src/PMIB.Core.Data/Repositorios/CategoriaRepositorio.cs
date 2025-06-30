using Microsoft.EntityFrameworkCore;
using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using PMIB.Core.Data.Context;

namespace PMIB.Core.Data.Repositorios;

public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
{
    public CategoriaRepositorio(PmibContext context) : base(context) { }

    public Task<Categoria> ObterPorNome(string nome)
    {
        return Db.Categorias.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Nome == nome);
    }
}
