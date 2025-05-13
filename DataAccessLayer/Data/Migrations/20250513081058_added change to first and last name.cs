using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedchangetofirstandlastname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Player2UserName",
                table: "Match",
                newName: "Player2LastName");

            migrationBuilder.RenameColumn(
                name: "Player1UserName",
                table: "Match",
                newName: "Player2FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Player1FirstName",
                table: "Match",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Player1LastName",
                table: "Match",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1FirstName",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "Player1LastName",
                table: "Match");

            migrationBuilder.RenameColumn(
                name: "Player2LastName",
                table: "Match",
                newName: "Player2UserName");

            migrationBuilder.RenameColumn(
                name: "Player2FirstName",
                table: "Match",
                newName: "Player1UserName");
        }
    }
}
