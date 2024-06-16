using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DragonRunes.Database.Migrations
{
    /// <inheritdoc />
    public partial class BirthDateSaltPositionDirectionBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hash",
                table: "Accounts",
                newName: "Salt");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Accounts",
                newName: "Hash");
        }
    }
}
