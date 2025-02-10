using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddtableThongTinCauHinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThongTinCauHinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    TenThonTin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    TenThongTin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThongBao = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    LoaiThongTin = table.Column<int>(type: "int", nullable: false),
                    KichThuocKyTu = table.Column<int>(type: "int", nullable: true),
                    IsBatBuocnhap = table.Column<bool>(type: "bit", nullable: false),
                    IsFileDinhKem = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_ThongTinCauHinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThongTinCauHinhs_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                        column: x => x.DanhSachQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinCauHinhBuocs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyNode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThongTinCauHinhId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinCauHinhBuocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThongTinCauHinhBuocs_ThongTinCauHinhs_ThongTinCauHinhId",
                        column: x => x.ThongTinCauHinhId,
                        principalSchema: "tempplate",
                        principalTable: "ThongTinCauHinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinCauHinhBuocs_ThongTinCauHinhId",
                schema: "tempplate",
                table: "ThongTinCauHinhBuocs",
                column: "ThongTinCauHinhId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinCauHinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "ThongTinCauHinhs",
                column: "DanhSachQuyTrinhId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongTinCauHinhBuocs",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "ThongTinCauHinhs",
                schema: "tempplate");
        }
    }
}
