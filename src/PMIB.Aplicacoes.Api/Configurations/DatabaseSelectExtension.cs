using Microsoft.EntityFrameworkCore;
using PMIB.Core.Data.Context;

namespace PMIB.Aplicacoes.Api.Configurations;

public static class DatabaseSelectExtension
{
    public static WebApplicationBuilder AddDatabaseSelector(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionLite") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<PmibContext>(options =>
            options.UseSqlite(connectionString));

            builder.Services.AddDbContext<PmibContext>(options =>
                options.UseSqlite(connectionString));
        }
        else
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<PmibContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<PmibContext>(options =>
                options.UseSqlServer(connectionString));
        }

        return builder;
    }
}
