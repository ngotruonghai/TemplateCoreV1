using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class UpdateTableNodeNextStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lineAttributes",
                schema: "tempplate",
                table: "DiagramNodes");

            migrationBuilder.RenameColumn(
                name: "type",
                schema: "tempplate",
                table: "Nodes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "position",
                schema: "tempplate",
                table: "Nodes",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "label",
                schema: "tempplate",
                table: "Nodes",
                newName: "TenNode");

            migrationBuilder.RenameColumn(
                name: "target",
                schema: "tempplate",
                table: "DiagramNodes",
                newName: "Target");

            migrationBuilder.RenameColumn(
                name: "source",
                schema: "tempplate",
                table: "DiagramNodes",
                newName: "Source");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                schema: "tempplate",
                table: "Nodes",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Target",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KeyId",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "TenDiagram",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NextStep",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NodeIdStart = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NodeIdEnd = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DiagramId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Action = table.Column<int>(type: "int", nullable: true),
                    TypeNextStep = table.Column<int>(type: "int", nullable: true),
                    DanhSachQuyTrinhId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NextStep_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                        column: x => x.DanhSachQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NextStep_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "NextStep",
                column: "DanhSachQuyTrinhId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NextStep",
                schema: "tempplate");

            migrationBuilder.DropColumn(
                name: "TenDiagram",
                schema: "tempplate",
                table: "DiagramNodes");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "tempplate",
                table: "Nodes",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "TenNode",
                schema: "tempplate",
                table: "Nodes",
                newName: "label");

            migrationBuilder.RenameColumn(
                name: "Index",
                schema: "tempplate",
                table: "Nodes",
                newName: "position");

            migrationBuilder.RenameColumn(
                name: "Target",
                schema: "tempplate",
                table: "DiagramNodes",
                newName: "target");

            migrationBuilder.RenameColumn(
                name: "Source",
                schema: "tempplate",
                table: "DiagramNodes",
                newName: "source");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                schema: "tempplate",
                table: "Nodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "target",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "source",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KeyId",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "lineAttributes",
                schema: "tempplate",
                table: "DiagramNodes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
