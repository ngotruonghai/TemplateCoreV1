using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class CreatetableCauHinhQuyTrinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaQuyTrinh",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "NoiDung",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThietLapMaPhieu",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PhongBanTheoDoiQuyTrinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhongbanId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenPhongBan = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoaiPhongBan = table.Column<int>(type: "int", nullable: false),
                    DanhSachQuyTrinhId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBanTheoDoiQuyTrinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhongBanTheoDoiQuyTrinhs_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                        column: x => x.DanhSachQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinNoiDungQuyTrinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThongTin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CauHinhHienThiBuoc = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    BatBuocNhap = table.Column<bool>(type: "bit", nullable: false),
                    FileDinhKem = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinNoiDungQuyTrinhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhanSuTheoDoiQuyTrinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoaiNhanSu = table.Column<int>(type: "int", nullable: false),
                    DanhSachQuyTrinhId = table.Column<int>(type: "int", nullable: true),
                    ThongTinNoiDungQuyTrinhId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanSuTheoDoiQuyTrinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanSuTheoDoiQuyTrinhs_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                        column: x => x.DanhSachQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NhanSuTheoDoiQuyTrinhs_ThongTinNoiDungQuyTrinhs_ThongTinNoiDungQuyTrinhId",
                        column: x => x.ThongTinNoiDungQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "ThongTinNoiDungQuyTrinhs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanSuTheoDoiQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs",
                column: "DanhSachQuyTrinhId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSuTheoDoiQuyTrinhs_ThongTinNoiDungQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs",
                column: "ThongTinNoiDungQuyTrinhId");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanTheoDoiQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "PhongBanTheoDoiQuyTrinhs",
                column: "DanhSachQuyTrinhId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhanSuTheoDoiQuyTrinhs",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "PhongBanTheoDoiQuyTrinhs",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "ThongTinNoiDungQuyTrinhs",
                schema: "tempplate");

            migrationBuilder.DropColumn(
                name: "NoiDung",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.DropColumn(
                name: "ThietLapMaPhieu",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.AlterColumn<string>(
                name: "MaQuyTrinh",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
