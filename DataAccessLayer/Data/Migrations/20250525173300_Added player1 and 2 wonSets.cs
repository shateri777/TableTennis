using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedplayer1and2wonSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Player1WonSets",
                table: "Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player2WonSets",
                table: "Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1WonSets",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Player2WonSets",
                table: "Sets");
        }
    }
}
