using FluxoDeCaixa.Core.Model;

namespace FluxoDeCaixa.Core.Interfaces;

public interface IFluxoDeCaixaService
{
    Task<LancamenetoFinanceiro> RealizarCreditoAsync(LancamenetoFinanceiro transacao);
    Task<LancamenetoFinanceiro> RealizarDebitoAsync(LancamenetoFinanceiro transacao);
    Task<decimal> ObterSaldoAsync();
}
