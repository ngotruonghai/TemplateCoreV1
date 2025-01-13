using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddTablePhongBan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "tempplate",
                table: "Nodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "tempplate",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "tempplate",
                table: "DiagramNodes");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");
        }
    }
}
