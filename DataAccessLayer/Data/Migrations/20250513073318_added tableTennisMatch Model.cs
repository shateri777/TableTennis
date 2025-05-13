using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedtableTennisMatchModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchDate",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Player1Age",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Player1UserName",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Player2Age",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Player2UserName",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "SetGender",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "WinnerPlayer",
                table: "Sets");

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player1UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player2UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player1Age = table.Column<int>(type: "int", nullable: false),
                    Player2Age = table.Column<int>(type: "int", nullable: false),
                    SetGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WinnerPlayer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.AddColumn<DateOnly>(
                name: "MatchDate",
                table: "Sets",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Player1Age",
                table: "Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Player1UserName",
                table: "Sets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Player2Age",
                table: "Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Player2UserName",
                table: "Sets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SetGender",
                table: "Sets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WinnerPlayer",
                table: "Sets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
