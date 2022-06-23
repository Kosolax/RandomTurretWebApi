using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomTurret.DataAccess.Migrations
{
    public partial class AddDifficultyMultiplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DifficultyMultiplier",
                table: "Waves",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DifficultyMultiplier",
                table: "Waves");
        }
    }
}
