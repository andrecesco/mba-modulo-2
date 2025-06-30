using Microsoft.EntityFrameworkCore;
using PMIB.Core.Data.Context;

namespace PMIB.Aplicacoes.Api.Configurations;

public static class DbMigrationsHelpers
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        EnsureSeedData(app).Wait();
    }

    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<PmibContext>();

        if (env.IsDevelopment())
        {
            await context.Database.MigrateAsync();

            await EnsureSeedProducts(context);
        }
    }

    private static async Task EnsureSeedProducts(PmibContext context)
    {
        //Realiza a carga inicial dos dados

        await context.SaveChangesAsync();
    }
}