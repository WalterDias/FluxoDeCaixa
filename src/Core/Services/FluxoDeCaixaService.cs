using FluxoDeCaixa.Core.DataBase;
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

    public async Task<LancamenetoFinanceiro> RealizarCreditoAsync(LancamenetoFinanceiro transacao)
    {
        if (transacao.Tipo != TipoLancamento.Credito)
            throw new ArgumentException("Tipo de transação inválido. Use 'Credito' para esta operação.");

        if (transacao.Valor <= 0)
            throw new ArgumentException("Valor da operação inválido");

        return await RealizarTransacaoAsync(transacao);
    }

    public async Task<LancamenetoFinanceiro> RealizarDebitoAsync(LancamenetoFinanceiro transacao)
    {
        if (transacao.Tipo != TipoLancamento.Debito)
            throw new ArgumentException("Tipo de transação inválido. Use 'Debito' para esta operação.");

        if (transacao.Valor >= 0)
            throw new ArgumentException("Valor da operação inválido");

        return await RealizarTransacaoAsync(transacao);
    }

    private async Task<LancamenetoFinanceiro> RealizarTransacaoAsync(LancamenetoFinanceiro transacao)
    {
        _context.LancamenetoFinanceiros.Add(transacao);

        await _context.SaveChangesAsync();

        return transacao;
    }

    public async Task<decimal> ObterSaldoAsync()
    {
        var saldo = await _context.LancamenetoFinanceiros
                .GroupBy(t => t.Tipo)
                .Select(g => new { Tipo = g.Key, Total = g.Sum(t => t.Valor) })
                .ToDictionaryAsync(g => g.Tipo, g => g.Total);

        var saldoFinal = saldo.GetValueOrDefault(TipoLancamento.Credito, 0) - saldo.GetValueOrDefault(TipoLancamento.Debito, 0);

        return saldoFinal;
    }
}
