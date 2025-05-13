using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedrelationbetweenmatchandset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Match_TableTennisMatchId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_TableTennisMatchId",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "TableTennisMatchId",
                table: "Sets");
        }
    }
}
