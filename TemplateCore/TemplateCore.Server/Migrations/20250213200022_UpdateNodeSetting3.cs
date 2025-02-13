using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNodeSetting3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NodeSettings_NodeId",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.CreateIndex(
                name: "IX_NodeSettings_NodeId",
                schema: "tempplate",
                table: "NodeSettings",
                column: "NodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NodeSettings_NodeId",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.CreateIndex(
                name: "IX_NodeSettings_NodeId",
                schema: "tempplate",
                table: "NodeSettings",
                column: "NodeId",
                unique: true,
                filter: "[NodeId] IS NOT NULL");
        }
    }
}
