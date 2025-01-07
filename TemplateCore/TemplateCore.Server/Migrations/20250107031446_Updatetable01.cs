using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class Updatetable01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyActiveId",
                schema: "tempplate",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentUser",
                schema: "tempplate",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeUser",
                schema: "tempplate",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyActiveId",
                schema: "tempplate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ParentUser",
                schema: "tempplate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TypeUser",
                schema: "tempplate",
                table: "User");
        }
    }
}
