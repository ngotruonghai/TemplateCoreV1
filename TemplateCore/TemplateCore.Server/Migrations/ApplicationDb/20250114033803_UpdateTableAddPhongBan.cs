using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class UpdateTableAddPhongBan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserParentId",
                schema: "tempplate",
                table: "PhongBans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserParentId",
                schema: "tempplate",
                table: "Nodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserParentId",
                schema: "tempplate",
                table: "KeyActives",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserParentId",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserParentId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserParentId",
                schema: "tempplate",
                table: "DanhMucs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserParentId",
                schema: "tempplate",
                table: "DanhMucPhanQuyens",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserParentId",
                schema: "tempplate",
                table: "PhongBans");

            migrationBuilder.DropColumn(
                name: "UserParentId",
                schema: "tempplate",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "UserParentId",
                schema: "tempplate",
                table: "KeyActives");

            migrationBuilder.DropColumn(
                name: "UserParentId",
                schema: "tempplate",
                table: "DiagramNodes");

            migrationBuilder.DropColumn(
                name: "UserParentId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.DropColumn(
                name: "UserParentId",
                schema: "tempplate",
                table: "DanhMucs");

            migrationBuilder.DropColumn(
                name: "UserParentId",
                schema: "tempplate",
                table: "DanhMucPhanQuyens");
        }
    }
}
