using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Core.Model;


public enum TipoLancamento : int
{
    Debito = 1,
    Credito = 2
}

[PrimaryKey(nameof(Id))]
[Index(nameof(Data), IsUnique = false)]
public record LancamenetoFinanceiro
{

    public Guid Id { get; set; }

    public DateTime Data { get; set; }

    public TipoLancamento Tipo { get; set; }

    public decimal Valor { get; set; }
}

public record LancamenetoFinanceiroView
{
    public Guid Id { get; set; }

    public DateTime Data { get; set; }

    public TipoLancamento Tipo { get; set; }

    public decimal Valor { get; set; }
}
