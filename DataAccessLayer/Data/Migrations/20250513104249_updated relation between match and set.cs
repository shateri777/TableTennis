using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedrelationbetweenmatchandset : Migration
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

            migrationBuilder.RenameColumn(
                name: "MatchId",
                table: "Sets",
                newName: "MatchIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_MatchIdId",
                table: "Sets",
                column: "MatchIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Match_MatchIdId",
                table: "Sets",
                column: "MatchIdId",
                principalTable: "Match",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Match_MatchIdId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_MatchIdId",
                table: "Sets");

            migrationBuilder.RenameColumn(
                name: "MatchIdId",
                table: "Sets",
                newName: "MatchId");

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
