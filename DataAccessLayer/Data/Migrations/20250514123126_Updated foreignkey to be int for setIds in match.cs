using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedforeignkeytobeintforsetIdsinmatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Match_TableTennisMatchId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_TableTennisMatchId",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "TableTennisMatchId",
                table: "Sets");

            migrationBuilder.AddColumn<string>(
                name: "SetIds",
                table: "Match",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetIds",
                table: "Match");

            migrationBuilder.AddColumn<int>(
                name: "TableTennisMatchId",
                table: "Sets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sets_TableTennisMatchId",
                table: "Sets",
                column: "TableTennisMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Match_TableTennisMatchId",
                table: "Sets",
                column: "TableTennisMatchId",
                principalTable: "Match",
                principalColumn: "Id");
        }
    }
}
