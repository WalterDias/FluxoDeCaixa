using FluxoDeCaixa.Core.Model;

namespace FluxoDeCaixa.Core.Interfaces;

public interface IRelatorioService
{
    Task<IEnumerable<LancamenetoFinanceiroView>> ListarLancamentos(DateTime dataInicio, DateTime dataFim);

    Task<IEnumerable<LancamenetoFinanceiroView>> ExtratoUltimos30Dias();

    Task<IEnumerable<LancamenetoFinanceiroView>> ExtratoDia();
}
