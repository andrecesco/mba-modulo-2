using Microsoft.AspNetCore.Identity;
using PMIB.Aplicacoes.Api.Extensions;
using PMIB.Core.Data.Context;

namespace PMIB.Aplicacoes.Api.Configurations;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<PmibContext>()
            .AddErrorDescriber<IdentityMensagensPortugues>();

        builder.Services.AddHttpContextAccessor();

        return builder;
    }
}

public static class ApiConfig
{
    public static WebApplicationBuilder AddApiConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        return builder;
    }
}