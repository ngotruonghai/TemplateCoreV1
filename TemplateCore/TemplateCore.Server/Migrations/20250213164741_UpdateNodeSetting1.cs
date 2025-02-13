using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNodeSetting1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_NodeSettings_NodeSettingId",
                schema: "tempplate",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_NodeSettingId",
                schema: "tempplate",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "NodeSettingId",
                schema: "tempplate",
                table: "Nodes");

            migrationBuilder.AddColumn<int>(
                name: "NodeId",
                schema: "tempplate",
                table: "NodeSettings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NodeSettings_NodeId",
                schema: "tempplate",
                table: "NodeSettings",
                column: "NodeId",
                unique: true,
                filter: "[NodeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_NodeSettings_Nodes_NodeId",
                schema: "tempplate",
                table: "NodeSettings",
                column: "NodeId",
                principalSchema: "tempplate",
                principalTable: "Nodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NodeSettings_Nodes_NodeId",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.DropIndex(
                name: "IX_NodeSettings_NodeId",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.DropColumn(
                name: "NodeId",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.AddColumn<int>(
                name: "NodeSettingId",
                schema: "tempplate",
                table: "Nodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_NodeSettingId",
                schema: "tempplate",
                table: "Nodes",
                column: "NodeSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_NodeSettings_NodeSettingId",
                schema: "tempplate",
                table: "Nodes",
                column: "NodeSettingId",
                principalSchema: "tempplate",
                principalTable: "NodeSettings",
                principalColumn: "Id");
        }
    }
}
