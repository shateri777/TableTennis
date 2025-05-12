using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSetModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
