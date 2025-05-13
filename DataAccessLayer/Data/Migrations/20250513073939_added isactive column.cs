using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TableTennis.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedisactivecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAcitve",
                table: "Sets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcitve",
                table: "Match",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAcitve",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "IsAcitve",
                table: "Match");
        }
    }
}
