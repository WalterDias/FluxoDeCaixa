using FluxoDeCaixa.Core.DataBase;
using FluxoDeCaixa.Core.Interfaces;
using FluxoDeCaixa.Core.Model;
using Microsoft.EntityFrameworkCore;


namespace FluxoDeCaixa.Core.Services;

/// <summary>
/// O serviço de relatorio foi implementando utilizando Exception como fluxo padrão
/// O motivo de se ter 3 implementaçãoe é poder ter comportamentos em um futuro dependendo da necessidade ex:
/// ExtratoDia - Busca direto do cache da memoria do servidor
/// ExtratoUltimos30Dias - busca do banco transacional
/// ListarLancamentos - buscaria do banco DW
/// </summary>
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


