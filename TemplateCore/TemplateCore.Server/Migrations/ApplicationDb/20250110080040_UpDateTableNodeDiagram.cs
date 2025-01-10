using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class UpDateTableNodeDiagram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhSachQuyTrinhDiagramNode",
                schema: "tempplate");

            migrationBuilder.DropColumn(
                name: "DiagramNodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.DropColumn(
                name: "NodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.CreateIndex(
                name: "IX_DiagramNodes_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "DiagramNodes",
                column: "DanhSachQuyTrinhId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagramNodes_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "DiagramNodes",
                column: "DanhSachQuyTrinhId",
                principalSchema: "tempplate",
                principalTable: "DanhSachQuyTrinhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagramNodes_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "DiagramNodes");

            migrationBuilder.DropIndex(
                name: "IX_DiagramNodes_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "DiagramNodes");

            migrationBuilder.AddColumn<int>(
                name: "DiagramNodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DanhSachQuyTrinhDiagramNode",
                schema: "tempplate",
                columns: table => new
                {
                    DanhSachQuyTrinhsId = table.Column<int>(type: "int", nullable: false),
                    DiagramNodesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachQuyTrinhDiagramNode", x => new { x.DanhSachQuyTrinhsId, x.DiagramNodesId });
                    table.ForeignKey(
                        name: "FK_DanhSachQuyTrinhDiagramNode_DanhSachQuyTrinhs_DanhSachQuyTrinhsId",
                        column: x => x.DanhSachQuyTrinhsId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhSachQuyTrinhDiagramNode_DiagramNodes_DiagramNodesId",
                        column: x => x.DiagramNodesId,
                        principalSchema: "tempplate",
                        principalTable: "DiagramNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachQuyTrinhDiagramNode_DiagramNodesId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhDiagramNode",
                column: "DiagramNodesId");
        }
    }
}
