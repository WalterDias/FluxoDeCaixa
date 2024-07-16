using FluxoDeCaixa.Core.DataBase;
using FluxoDeCaixa.Core.Framework;
using FluxoDeCaixa.Core.Interfaces;
using FluxoDeCaixa.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Core.Services;

public class FluxoDeCaixaService : IFluxoDeCaixaService
{
    private readonly FluxoDeCaixaContext _context;

    public FluxoDeCaixaService(FluxoDeCaixaContext context)
    {
        _context = context;
    }

    public async Task<Result<LancamenetoFinanceiro>> RealizarCreditoAsync(LancamenetoFinanceiro transacao)
    {
        if (transacao.Tipo != TipoLancamento.Credito)
            return Result.Failure<LancamenetoFinanceiro>(BusinessErros.FluxoDeCaixaErros.TipoLancamentoCreditoInvalido);

        if (transacao.Valor <= 0)
            return Result.Failure<LancamenetoFinanceiro>(BusinessErros.FluxoDeCaixaErros.ValorDaOperacaoInvalido);

        return await RealizarTransacaoAsync(transacao);
    }

    public async Task<Result<LancamenetoFinanceiro>> RealizarDebitoAsync(LancamenetoFinanceiro transacao)
    {
        if (transacao.Tipo != TipoLancamento.Debito)
            return Result.Failure<LancamenetoFinanceiro>(BusinessErros.FluxoDeCaixaErros.TipoLancamentoCreditoInvalido);

        if (transacao.Valor >= 0)
            return Result.Failure<LancamenetoFinanceiro>(BusinessErros.FluxoDeCaixaErros.ValorDaOperacaoInvalido);

        return await RealizarTransacaoAsync(transacao);
    }

    private async Task<Result<LancamenetoFinanceiro>> RealizarTransacaoAsync(LancamenetoFinanceiro transacao)
    {
        if (transacao.Data is null)
            return Result.Failure<LancamenetoFinanceiro>(BusinessErros.FluxoDeCaixaErros.DataDaOperacaoInvalido);

        _context.LancamenetoFinanceiros.Add(transacao);

        await _context.SaveChangesAsync();

        return Result.Success(transacao);
    }

    public async Task<Result<decimal>> ObterSaldoAsync()
    {
        var saldo = await _context.LancamenetoFinanceiros
                .GroupBy(t => t.Tipo)
                .Select(g => new { Tipo = g.Key, Total = g.Sum(t => t.Valor) })
                .ToDictionaryAsync(g => g.Tipo, g => g.Total);

        var saldoFinal = saldo.GetValueOrDefault(TipoLancamento.Credito, 0) + saldo.GetValueOrDefault(TipoLancamento.Debito, 0);

        return Result.Success(saldoFinal);
    }    
}
