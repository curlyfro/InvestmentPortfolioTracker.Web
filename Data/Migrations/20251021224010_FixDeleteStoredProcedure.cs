using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentPortfolioTracker.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDeleteStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop and recreate sp_DeleteHolding to properly return rows affected
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_DeleteHolding");
            
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_DeleteHolding
                    @Id INT
                AS
                BEGIN
                    DELETE FROM Holdings
                    WHERE Id = @Id;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Restore original sp_DeleteHolding with SELECT statement
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_DeleteHolding");
            
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_DeleteHolding
                    @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    DELETE FROM Holdings
                    WHERE Id = @Id;
                    
                    SELECT @@ROWCOUNT AS RowsAffected;
                END
            ");
        }
    }
}
