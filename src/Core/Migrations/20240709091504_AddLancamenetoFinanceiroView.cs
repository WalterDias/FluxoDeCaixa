using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoDeCaixa.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddLancamenetoFinanceiroView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW vw_LancamenetoFinanceiros AS (
                SELECT 
                    Id,
                    Data,
                    Tipo,
                    Valor
                FROM 
                    LancamenetoFinanceiros);

                GO
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP VIEW IF EXISTS vw_LancamenetoFinanceiros;
            ");
        }
    }
}
