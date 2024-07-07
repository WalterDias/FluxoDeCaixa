using FluxoDeCaixa.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Core.DataBase;

public class FluxoDeCaixaContext : DbContext
{
    public FluxoDeCaixaContext(DbContextOptions<FluxoDeCaixaContext> options)
        : base(options)
    {
    }

    public DbSet<LancamenetoFinanceiro> LancamenetoFinanceiros { get; set; } = null!;
}