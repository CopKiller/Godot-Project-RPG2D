using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DragonRunes.Database.Migrations
{
    /// <inheritdoc />
    public partial class AlteradoMailParaEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Accounts",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Accounts",
                newName: "Mail");
        }
    }
}
