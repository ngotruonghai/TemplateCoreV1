using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddTableNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KeyACtiveCode",
                schema: "tempplate",
                table: "KeyActives",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "DiagramNodes",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    lineAttributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DanhSachQuyTrinhId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagramNodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    label = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DanhSachQuyTrinhId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachQuyTrinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaQuyTrinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenQuyTrinh = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiagramNodeId = table.Column<int>(type: "int", nullable: true),
                    NodeId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachQuyTrinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhSachQuyTrinhs_DiagramNodes_DiagramNodeId",
                        column: x => x.DiagramNodeId,
                        principalSchema: "tempplate",
                        principalTable: "DiagramNodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DanhSachQuyTrinhs_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalSchema: "tempplate",
                        principalTable: "Nodes",
                        principalColumn: "Id");
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhSachQuyTrinhs",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "DiagramNodes",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "Nodes",
                schema: "tempplate");

            migrationBuilder.AlterColumn<string>(
                name: "KeyACtiveCode",
                schema: "tempplate",
                table: "KeyActives",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
        }
    }
}
