using FluxoDeCaixa.Core.Framework;
using System.Drawing;

namespace FluxoDeCaixa.Core.BusinessErros;

public static class FluxoDeCaixaErros
{
    public static Error TipoLancamentoCreditoInvalido { get => new Error("400.1", "Tipo de transação inválido. Use 'Credito' para esta operação."); }

    public static Error TipoLancamentoDebitoInvalido { get => new Error("400.2", "Tipo de transação inválido. Use 'Debito' para esta operação."); }

    public static Error ValorDaOperacaoInvalido { get => new Error("400.3", "Valor da operação inválido"); }

    public static Error DataDaOperacaoInvalido { get => new Error("400.4", "A data da operação inválido"); }
}
