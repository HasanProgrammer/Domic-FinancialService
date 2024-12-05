using Domic.Infrastructure.Implementations.UseCase.Services;
using Domic.UseCase.TransactionUseCase.Contracts.Interfaces;

namespace Domic.WebAPI.Frameworks.Extensions;

public static class IApplicationBuilderExtension
{
    public static void RegisterBankGateway(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IZarinPalBankGateway, ZarinPalBankGateway>();
    }
}