using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableLichSuQuyTrinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichSuQuyTrinh_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "LichSuQuyTrinh");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LichSuQuyTrinh",
                schema: "tempplate",
                table: "LichSuQuyTrinh");

            migrationBuilder.RenameTable(
                name: "LichSuQuyTrinh",
                schema: "tempplate",
                newName: "LichSuQuyTrinhs",
                newSchema: "tempplate");

            migrationBuilder.RenameIndex(
                name: "IX_LichSuQuyTrinh_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "LichSuQuyTrinhs",
                newName: "IX_LichSuQuyTrinhs_DanhSachQuyTrinhId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LichSuQuyTrinhs",
                schema: "tempplate",
                table: "LichSuQuyTrinhs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuQuyTrinhs_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "LichSuQuyTrinhs",
                column: "DanhSachQuyTrinhId",
                principalSchema: "tempplate",
                principalTable: "DanhSachQuyTrinhs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichSuQuyTrinhs_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "LichSuQuyTrinhs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LichSuQuyTrinhs",
                schema: "tempplate",
                table: "LichSuQuyTrinhs");

            migrationBuilder.RenameTable(
                name: "LichSuQuyTrinhs",
                schema: "tempplate",
                newName: "LichSuQuyTrinh",
                newSchema: "tempplate");

            migrationBuilder.RenameIndex(
                name: "IX_LichSuQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "LichSuQuyTrinh",
                newName: "IX_LichSuQuyTrinh_DanhSachQuyTrinhId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LichSuQuyTrinh",
                schema: "tempplate",
                table: "LichSuQuyTrinh",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuQuyTrinh_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "LichSuQuyTrinh",
                column: "DanhSachQuyTrinhId",
                principalSchema: "tempplate",
                principalTable: "DanhSachQuyTrinhs",
                principalColumn: "Id");
        }
    }
}
