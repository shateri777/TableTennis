using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updatedrealatinshipbetweensetsandmatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetIds",
                table: "Match");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_MatchId",
                table: "Sets",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Match_MatchId",
                table: "Sets",
                column: "MatchId",
                principalTable: "Match",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Match_MatchId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_MatchId",
                table: "Sets");

            migrationBuilder.AddColumn<string>(
                name: "SetIds",
                table: "Match",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
