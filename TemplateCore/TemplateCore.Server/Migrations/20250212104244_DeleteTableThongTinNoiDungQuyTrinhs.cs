using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTableThongTinNoiDungQuyTrinhs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhanSuTheoDoiQuyTrinhs_ThongTinNoiDungQuyTrinhs_ThongTinNoiDungQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs");

            migrationBuilder.DropTable(
                name: "ThongTinNoiDungQuyTrinhs",
                schema: "tempplate");

            migrationBuilder.DropIndex(
                name: "IX_NhanSuTheoDoiQuyTrinhs_ThongTinNoiDungQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs");

            migrationBuilder.DropColumn(
                name: "TenThongTin",
                schema: "tempplate",
                table: "ThongTinCauHinhs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TenThongTin",
                schema: "tempplate",
                table: "ThongTinCauHinhs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ThongTinNoiDungQuyTrinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatBuocNhap = table.Column<bool>(type: "bit", nullable: false),
                    CauHinhHienThiBuoc = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FileDinhKem = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    TenThongTin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinNoiDungQuyTrinhs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanSuTheoDoiQuyTrinhs_ThongTinNoiDungQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs",
                column: "ThongTinNoiDungQuyTrinhId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhanSuTheoDoiQuyTrinhs_ThongTinNoiDungQuyTrinhs_ThongTinNoiDungQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs",
                column: "ThongTinNoiDungQuyTrinhId",
                principalSchema: "tempplate",
                principalTable: "ThongTinNoiDungQuyTrinhs",
                principalColumn: "Id");
        }
    }
}
