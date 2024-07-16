using FluxoDeCaixa.Core.Framework;
using FluxoDeCaixa.Core.Model;

namespace FluxoDeCaixa.Core.Interfaces;

public interface IFluxoDeCaixaService
{
    Task<Result<LancamenetoFinanceiro>> RealizarCreditoAsync(LancamenetoFinanceiro transacao);
    Task<Result<LancamenetoFinanceiro>> RealizarDebitoAsync(LancamenetoFinanceiro transacao);
    Task<Result<decimal>> ObterSaldoAsync();
}
