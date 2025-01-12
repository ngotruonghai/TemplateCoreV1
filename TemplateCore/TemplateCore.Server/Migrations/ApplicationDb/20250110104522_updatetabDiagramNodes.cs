using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class updatetabDiagramNodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "target",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "target",
                schema: "tempplate",
                table: "DiagramNodes");
        }
    }
}
