using FluxoDeCaixa.Core.Framework;
using FluxoDeCaixa.Core.Interfaces;
using FluxoDeCaixa.Core.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FluxoDeCaixa.API.ServicesExtension;

public static class FluxoDeCaixaServiceExtension
{
    public static void MapearRotasServicoFluxoDeCaixa(this IEndpointRouteBuilder app)
    {
        app.MapPost("/caixa/creditar", async (LancamenetoFinanceiro transacao, IFluxoDeCaixaService service) =>
        {
            var result = await service.RealizarCreditoAsync(transacao);

            return result.Match(
               onSuccess: () => Results.Ok(result.Value),
               onFailure: error => Results.BadRequest(error));

        });

        app.MapPost("/caixa/debitar", async (LancamenetoFinanceiro transacao, IFluxoDeCaixaService service) =>
        {
            var result = await service.RealizarDebitoAsync(transacao);

            return result.Match(
               onSuccess: () => Results.Ok(result.Value),
               onFailure: error => Results.BadRequest(error));
        });

        app.MapGet("/caixa/saldo", async (IFluxoDeCaixaService service) =>
        {
            var result = await service.ObterSaldoAsync();

            return result.Match(
               onSuccess: () => Results.Ok(result.Value),
               onFailure: error => Results.BadRequest(error));
        });
    }
}
