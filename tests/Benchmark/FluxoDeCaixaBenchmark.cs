using FluxoDeCaixa.Core.Model;
using BenchmarkDotNet.Attributes;
using FluxoDeCaixa.Core.DataBase;
using FluxoDeCaixa.Core.Services;
using Microsoft.EntityFrameworkCore;

[MemoryDiagnoser(true)]
public class FluxoDeCaixaBenchmark
{
    private FluxoDeCaixaContext _context;
    private FluxoDeCaixaService _service;

    [GlobalSetup]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FluxoDeCaixaContext>()
            .UseInMemoryDatabase(databaseName: "FluxoDeCaixaBenchmark")
            .Options;

        _context = new FluxoDeCaixaContext(options);
        _service = new FluxoDeCaixaService(_context);
    }

    [Benchmark]
    public async Task RealizarCreditoBenchmark()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Credito,
            Valor = 100m
        };

        await _service.RealizarCreditoAsync(transacao);
    }

    [Benchmark]
    public async Task RealizarDebitoBenchmark()
    {
        var transacao = new LancamenetoFinanceiro
        {
            Tipo = TipoLancamento.Debito,
            Valor = -50m
        };

        await _service.RealizarDebitoAsync(transacao);
    }

    [Benchmark()]
    public async Task ObterSaldoBenchmark()
    {
        await _service.ObterSaldoAsync();
    }
}