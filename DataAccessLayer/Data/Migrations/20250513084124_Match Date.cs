using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class MatchDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MatchDate",
                table: "Match",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SetCounter",
                table: "Match",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchDate",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "SetCounter",
                table: "Match");
        }
    }
}
