using FluxoDeCaixa.Core.Interfaces;
using FluxoDeCaixa.Core.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace FluxoDeCaixa.API.ServicesExtension;

public static class RelatorioServiceExtension
{
    public static void MapearRotasServicoRelatorio(this IEndpointRouteBuilder app)
    {

        app.MapPost("/relatorio/ultimodia", async (IRelatorioService service) => await GenericReportMethod(app, service, DateTime.Now.AddDays(-1), DateTime.Now));

        app.MapPost("/relatorio/ultimos30dias", async (IRelatorioService service) => await GenericReportMethod(app, service, DateTime.Now.AddDays(-30), DateTime.Now));

        app.MapPost("/relatorio/listar", async (DateTime dataInicio, DateTime dataFim, IRelatorioService service) => await GenericReportMethod(app, service, dataInicio, dataFim));
    }

    private static async Task<IResult> GenericReportMethod(IEndpointRouteBuilder app, IRelatorioService service,  DateTime dataInicio, DateTime dataFim)
    {
        try
        {
            var result = await service.ListarLancamentos(dataInicio, dataFim);
            return Results.Ok(result);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        catch
        {
            // logar o erro interno com um APM
            return Results.StatusCode(500);
        }
    }
}
