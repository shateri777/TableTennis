using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updatedsetsentity : Migration
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
                name: "Player1FirstName",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Player1LastName",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Player2Age",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Player2FirstName",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "Player2LastName",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "SetGender",
                table: "Sets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MatchDate",
                table: "Sets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Player1Age",
                table: "Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Player1FirstName",
                table: "Sets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Player1LastName",
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
                name: "Player2FirstName",
                table: "Sets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Player2LastName",
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
        }
    }
}
