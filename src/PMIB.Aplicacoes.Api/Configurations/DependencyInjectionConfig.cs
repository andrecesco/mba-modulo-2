using PMIB.Core.Business.Interfaces;
using PMIB.Core.Data.Context;
using PMIB.Core.Data.Repositorios;

namespace PMIB.Aplicacoes.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<PmibContext>();

        builder.Services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
        builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
        builder.Services.AddScoped<IVendedorRepositorio, VendedorRepositorio>();
        builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

        return builder;
    }
}
