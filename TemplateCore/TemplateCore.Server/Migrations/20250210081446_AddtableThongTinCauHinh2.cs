using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddtableThongTinCauHinh2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanhMucPhanQuyens_DanhMucs_DanhMucId1",
                schema: "tempplate",
                table: "DanhMucPhanQuyens");

            migrationBuilder.DropIndex(
                name: "IX_DanhMucPhanQuyens_DanhMucId1",
                schema: "tempplate",
                table: "DanhMucPhanQuyens");

            migrationBuilder.DropColumn(
                name: "DanhMucId1",
                schema: "tempplate",
                table: "DanhMucPhanQuyens");

            migrationBuilder.AlterColumn<int>(
                name: "DanhMucId",
                schema: "tempplate",
                table: "DanhMucPhanQuyens",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucPhanQuyens_DanhMucId",
                schema: "tempplate",
                table: "DanhMucPhanQuyens",
                column: "DanhMucId");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhMucPhanQuyens_DanhMucs_DanhMucId",
                schema: "tempplate",
                table: "DanhMucPhanQuyens",
                column: "DanhMucId",
                principalSchema: "tempplate",
                principalTable: "DanhMucs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanhMucPhanQuyens_DanhMucs_DanhMucId",
                schema: "tempplate",
                table: "DanhMucPhanQuyens");

            migrationBuilder.DropIndex(
                name: "IX_DanhMucPhanQuyens_DanhMucId",
                schema: "tempplate",
                table: "DanhMucPhanQuyens");

            migrationBuilder.AlterColumn<string>(
                name: "DanhMucId",
                schema: "tempplate",
                table: "DanhMucPhanQuyens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DanhMucId1",
                schema: "tempplate",
                table: "DanhMucPhanQuyens",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucPhanQuyens_DanhMucId1",
                schema: "tempplate",
                table: "DanhMucPhanQuyens",
                column: "DanhMucId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhMucPhanQuyens_DanhMucs_DanhMucId1",
                schema: "tempplate",
                table: "DanhMucPhanQuyens",
                column: "DanhMucId1",
                principalSchema: "tempplate",
                principalTable: "DanhMucs",
                principalColumn: "Id");
        }
    }
}
