﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomTurret.DataAccess.Migrations
{
    public partial class AddPlayerTowerAndTowerStat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Gold = table.Column<int>(type: "integer", nullable: false),
                    Pseudo = table.Column<string>(type: "varchar(20)", nullable: false),
                    Mail = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Towers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    TowerType = table.Column<int>(type: "int", nullable: false),
                    PlayerEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Towers_Players_PlayerEntityId",
                        column: x => x.PlayerEntityId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TowerStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StatEntityId = table.Column<int>(type: "int", nullable: false),
                    TowerEntityId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TowerStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TowerStats_Stats_StatEntityId",
                        column: x => x.StatEntityId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TowerStats_Towers_TowerEntityId",
                        column: x => x.TowerEntityId,
                        principalTable: "Towers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Towers_PlayerEntityId",
                table: "Towers",
                column: "PlayerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TowerStats_StatEntityId",
                table: "TowerStats",
                column: "StatEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TowerStats_TowerEntityId",
                table: "TowerStats",
                column: "TowerEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TowerStats");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Towers");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
