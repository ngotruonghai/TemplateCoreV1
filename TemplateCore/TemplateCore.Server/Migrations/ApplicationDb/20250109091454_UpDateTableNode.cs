using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class UpDateTableNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanhSachQuyTrinhs_DiagramNodes_DiagramNodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhSachQuyTrinhs_Nodes_NodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.DropIndex(
                name: "IX_DanhSachQuyTrinhs_DiagramNodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.DropIndex(
                name: "IX_DanhSachQuyTrinhs_NodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs");

            migrationBuilder.AlterColumn<int>(
                name: "NodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiagramNodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "IX_Nodes_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "Nodes",
                column: "DanhSachQuyTrinhId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachQuyTrinhDiagramNode_DiagramNodesId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhDiagramNode",
                column: "DiagramNodesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "Nodes",
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
                name: "FK_Nodes_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "Nodes");

            migrationBuilder.DropTable(
                name: "DanhSachQuyTrinhDiagramNode",
                schema: "tempplate");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "Nodes");

            migrationBuilder.AlterColumn<int>(
                name: "NodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DiagramNodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachQuyTrinhs_DiagramNodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                column: "DiagramNodeId");

            migrationBuilder.CreateIndex(
                name: "IX_DanhSachQuyTrinhs_NodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                column: "NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhSachQuyTrinhs_DiagramNodes_DiagramNodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                column: "DiagramNodeId",
                principalSchema: "tempplate",
                principalTable: "DiagramNodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhSachQuyTrinhs_Nodes_NodeId",
                schema: "tempplate",
                table: "DanhSachQuyTrinhs",
                column: "NodeId",
                principalSchema: "tempplate",
                principalTable: "Nodes",
                principalColumn: "Id");
        }
    }
}
