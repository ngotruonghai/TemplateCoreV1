using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class updateDanhSachQuyTrinh03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDau",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GhiChu",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.DropColumn(
                name: "NgayBatDau",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");
        }
    }
}
