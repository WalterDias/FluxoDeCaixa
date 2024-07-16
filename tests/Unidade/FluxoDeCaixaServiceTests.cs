using FluxoDeCaixa.Core.BusinessErros;
using FluxoDeCaixa.Core.DataBase;
using FluxoDeCaixa.Core.Framework;
using FluxoDeCaixa.Core.Model;
using FluxoDeCaixa.Core.Services;
using Microsoft.EntityFrameworkCore;

public class FluxoDeCaixaServiceTests
{
    private readonly FluxoDeCaixaContext _context;
    private readonly FluxoDeCaixaService _service;

    public FluxoDeCaixaServiceTests()
    {
        var options = new DbContextOptionsBuilder<FluxoDeCaixaContext>()
            .UseInMemoryDatabase(databaseName: "FluxoDeCaixaTest")
            .Options;

        _context = new FluxoDeCaixaContext(options);
        _service = new FluxoDeCaixaService(_context);
    }

    [Fact]
    public async Task RealizarCreditoAsync_DeveAdicionarCredito()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Credito,
            Valor = 100m,
            Data = DateTime.Now
        };

        var result = await _service.RealizarCreditoAsync(transacao);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task RealizarCreditoAsync_NaoDeveAdicionarPorProblemaTipoLancamento()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = -50m,
            Data = DateTime.Now
        };

        var result = await _service.RealizarCreditoAsync(transacao);

        Assert.False(result.IsSuccess);
        Assert.Equal(FluxoDeCaixaErros.TipoLancamentoCreditoInvalido, result.Error);
    }

    [Fact]
    public async Task RealizarCreditoAsync_NaoDeveAdicionarPorProblemaValor()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Credito,
            Valor = -50m,
            Data = DateTime.Now
        };

        var result = await _service.RealizarCreditoAsync(transacao);

        Assert.False(result.IsSuccess);
        Assert.Equal(FluxoDeCaixaErros.ValorDaOperacaoInvalido, result.Error);
    }

    [Fact]
    public async Task RealizarDebitoAsync_DeveAdicionarDebito()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = -50m,
            Data = DateTime.Now
        };

        var result = await _service.RealizarDebitoAsync(transacao);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task RealizarDebitoAsync_NaoDeveAdicionarPorProblemaTipoLancamento()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Credito,
            Valor = -50m,
            Data = DateTime.Now
        };

        var result = await _service.RealizarDebitoAsync(transacao);

        Assert.False(result.IsSuccess);
        Assert.Equal(FluxoDeCaixaErros.TipoLancamentoCreditoInvalido, result.Error);
    }

    [Fact]
    public async Task RealizarDebitoAsync_NaoDeveAdicionarPorProblemaValor()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = 50m,
            Data = DateTime.Now
        };

        var result = await _service.RealizarDebitoAsync(transacao);

        Assert.False(result.IsSuccess);
        Assert.Equal(FluxoDeCaixaErros.ValorDaOperacaoInvalido, result.Error);
    }

    [Fact]
    public async Task RealizarDebitoAsync_NaoDeveAdicionarPorProblemaData()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = 50m,
            Data = null
        };

        var result = await _service.RealizarDebitoAsync(transacao);

        Assert.False(result.IsSuccess);
        Assert.Equal(FluxoDeCaixaErros.DataDaOperacaoInvalido, result.Error);
    }

    [Fact]
    public async Task ObterSaldoAsync_DeveRetornarSaldoCorreto()
    {
        var credito = _context.LancamenetoFinanceiros.Add(new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Credito,
            Valor = 200m,
            Data = DateTime.Now
        });

        var debito = _context.LancamenetoFinanceiros.Add(new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = -50m,
            Data = DateTime.Now.AddMinutes(2)
        });

        await _context.SaveChangesAsync();

        var saldo = await _service.ObterSaldoAsync();

        Assert.True(saldo.IsSuccess);
        Assert.Equal(150m, saldo.Value);
    }
}
