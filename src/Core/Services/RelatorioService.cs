using FluxoDeCaixa.Core.DataBase;
using FluxoDeCaixa.Core.Interfaces;
using FluxoDeCaixa.Core.Model;
using Microsoft.EntityFrameworkCore;


namespace FluxoDeCaixa.Core.Services;

public class RelatorioService : IRelatorioService
{
    private readonly FluxoDeCaixaContext _context;

    public RelatorioService(FluxoDeCaixaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LancamenetoFinanceiroView>> ExtratoDia() => await ListarLancamentos(DateTime.Now.AddDays(-1), DateTime.Now);

    public async Task<IEnumerable<LancamenetoFinanceiroView>> ExtratoUltimos30Dias() => await this.ListarLancamentos(DateTime.Now.AddDays(-30), DateTime.Now);
    
    public async Task<IEnumerable<LancamenetoFinanceiroView>> ListarLancamentos(DateTime dataInicio, DateTime dataFim)
    {
        return await _context.LancamenetoFinanceiroView
            .Where(v => v.Data >= dataInicio && v.Data <= dataFim)
            .ToListAsync();
    }
}


