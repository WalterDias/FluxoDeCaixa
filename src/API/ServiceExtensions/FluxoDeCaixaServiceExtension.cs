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
            try
            {
                var result = await service.RealizarCreditoAsync(transacao);
                return Results.Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch ( Exception ex )
            {
                // logar o erro interno com um APM
                return Results.StatusCode(500);
            }
        });

        app.MapPost("/caixa/debitar", async (LancamenetoFinanceiro transacao, IFluxoDeCaixaService service) =>
        {
            try
            {
                var result = await service.RealizarDebitoAsync(transacao);
                return Results.Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch ( Exception ex )
            {
                // logar o erro interno com um APM
                return Results.StatusCode(500);
            }
        });

        app.MapGet("/caixa/saldo", async (IFluxoDeCaixaService service) =>
        {
            try
            {
                var result = await service.ObterSaldoAsync();
                return Results.Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // logar o erro interno com um APM
                return Results.StatusCode(500);
            }
        });
    }
}
