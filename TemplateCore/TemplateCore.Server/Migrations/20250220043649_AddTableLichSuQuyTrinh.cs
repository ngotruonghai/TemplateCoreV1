using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTableLichSuQuyTrinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LichSuQuyTrinh",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DanhSachQuyTrinh = table.Column<int>(type: "int", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    DanhSachQuyTrinhId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuQuyTrinh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichSuQuyTrinh_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                        column: x => x.DanhSachQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LichSuQuyTrinh_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "LichSuQuyTrinh",
                column: "DanhSachQuyTrinhId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LichSuQuyTrinh",
                schema: "tempplate");
        }
    }
}
