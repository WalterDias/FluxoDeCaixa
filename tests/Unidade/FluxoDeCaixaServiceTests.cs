using FluxoDeCaixa.Core.DataBase;
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
            Valor = 100m
        };

        var result = await _service.RealizarCreditoAsync(transacao);

        Assert.NotNull(result);
        Assert.Equal(transacao.Tipo, result.Tipo);
        Assert.Equal(transacao.Valor, result.Valor);
    }

    [Fact]
    public void RealizarCreditoAsync_NaoDeveAdicionarPorProblemaTipoLancamento()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = -50m
        };

        var exception = Assert.ThrowsAsync<ArgumentException>(() => _service.RealizarCreditoAsync(transacao));
        Assert.Equal("Tipo de transação inválido. Use 'Credito' para esta operação.", exception?.Result.Message);
    }

    [Fact]
    public void RealizarCreditoAsync_NaoDeveAdicionarPorProblemaValor()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Credito,
            Valor = -50m
        };

        var exception = Assert.ThrowsAsync<ArgumentException>(() => _service.RealizarCreditoAsync(transacao));
        Assert.Equal("Valor da operação inválido", exception?.Result.Message);
    }

    [Fact]
    public async Task RealizarDebitoAsync_DeveAdicionarDebito()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = -50m
        };

        var result = await _service.RealizarDebitoAsync(transacao);

        Assert.NotNull(result);
        Assert.Equal(transacao.Tipo, result.Tipo);
        Assert.Equal(transacao.Valor, result.Valor);
    }

    [Fact]
    public void RealizarDebitoAsync_NaoDeveAdicionarPorProblemaTipoLancamento()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Credito,
            Valor = -50m
        };

        var exception = Assert.ThrowsAsync<ArgumentException>(() => _service.RealizarDebitoAsync(transacao));
        Assert.Equal("Tipo de transação inválido. Use 'Debito' para esta operação.", exception?.Result.Message);
    }

    [Fact]
    public void RealizarDebitoAsync_NaoDeveAdicionarPorProblemaValor()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = 50m
        };

        var exception = Assert.ThrowsAsync<ArgumentException>(() => _service.RealizarDebitoAsync(transacao));
        Assert.Equal("Valor da operação inválido", exception?.Result.Message);
    }

    [Fact]
    public async Task ObterSaldoAsync_DeveRetornarSaldoCorreto()
    {
        var credito = _context.LancamenetoFinanceiros.Add(new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Credito,
            Valor = 200m
        });

        var debito = _context.LancamenetoFinanceiros.Add(new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = -50m
        });

        await _context.SaveChangesAsync();

        var saldo = await _service.ObterSaldoAsync();

        Assert.Equal(150m, saldo);
    }
}
