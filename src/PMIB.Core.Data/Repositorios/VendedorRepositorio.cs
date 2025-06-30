using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using PMIB.Core.Data.Context;

namespace PMIB.Core.Data.Repositorios;

public class VendedorRepositorio : Repositorio<Vendedor>, IVendedorRepositorio
{
    public VendedorRepositorio(PmibContext context) : base(context) { }
}
