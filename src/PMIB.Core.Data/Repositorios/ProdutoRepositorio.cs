using Microsoft.EntityFrameworkCore;
using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using PMIB.Core.Data.Context;

namespace PMIB.Core.Data.Repositorios;

public class ProdutoRepositorio : Repositorio<Produto>, IProdutoRepositorio
{
    public ProdutoRepositorio(PmibContext context) : base(context) { }

    public async Task<List<Produto>> ObterPorCategoriaId(Guid categoriaId)
    {
        return await Db.Produtos.AsNoTracking()
            .Include(p => p.Categoria)
            .Where(p => p.CategoriaId == categoriaId)
            .ToListAsync();
    }

    public Task<List<Produto>> ObterPorVendedorId(Guid vendedorId)
    {
        return Db.Produtos.AsNoTracking()
            .Include(p => p.Vendedor)
            .Where(p => p.VendedorId == vendedorId)
            .ToListAsync();
    }
}
