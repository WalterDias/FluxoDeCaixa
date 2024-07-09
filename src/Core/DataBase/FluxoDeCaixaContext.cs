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

    public DbSet<LancamenetoFinanceiroView> LancamenetoFinanceiroView { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar a view como uma entidade de leitura
        modelBuilder.Entity<LancamenetoFinanceiroView>()
            .ToView("vw_LancamenetoFinanceiros")
            .HasKey(v => v.Id);


        modelBuilder.Entity<LancamenetoFinanceiroView>()
                .HasIndex(v => v.Data)
                .HasDatabaseName("IX_vw_LancamenetoFinanceiros_Data");
    }
}