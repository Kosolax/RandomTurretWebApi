﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomTurret.DataAccess.Migrations
{
    public partial class UpdateDoubleToFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "DifficultyMultiplier",
                table: "Waves",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "TowerStats",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "MobStats",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DifficultyMultiplier",
                table: "Waves",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "TowerStats",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "MobStats",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }
    }
}
