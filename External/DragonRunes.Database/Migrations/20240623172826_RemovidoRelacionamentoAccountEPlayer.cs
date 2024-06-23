using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DragonRunes.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovidoRelacionamentoAccountEPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Players_PlayerId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PlayerId",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PlayerId",
                table: "Accounts",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Players_PlayerId",
                table: "Accounts",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
