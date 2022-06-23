using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomTurret.DataAccess.Migrations
{
    public partial class AddWavesAndWavesMobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Waves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WaveNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WavesMobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MobEntityId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    WaveEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WavesMobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WavesMobs_Mobs_MobEntityId",
                        column: x => x.MobEntityId,
                        principalTable: "Mobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WavesMobs_Waves_WaveEntityId",
                        column: x => x.WaveEntityId,
                        principalTable: "Waves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WavesMobs_MobEntityId",
                table: "WavesMobs",
                column: "MobEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_WavesMobs_WaveEntityId",
                table: "WavesMobs",
                column: "WaveEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WavesMobs");

            migrationBuilder.DropTable(
                name: "Waves");
        }
    }
}
