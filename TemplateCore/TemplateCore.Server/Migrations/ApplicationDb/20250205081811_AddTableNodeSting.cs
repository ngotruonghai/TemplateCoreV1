using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddTableNodeSting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_NodeSetting_NodeSettingId",
                schema: "tempplate",
                table: "Nodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NodeSetting",
                schema: "tempplate",
                table: "NodeSetting");

            migrationBuilder.RenameTable(
                name: "NodeSetting",
                schema: "tempplate",
                newName: "NodeSettings",
                newSchema: "tempplate");

            migrationBuilder.AlterColumn<int>(
                name: "PhongbanId",
                schema: "tempplate",
                table: "PhongBanTheoDoiQuyTrinhs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CauHinhMailNhacNho",
                schema: "tempplate",
                table: "NodeSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                schema: "tempplate",
                table: "NodeSettings",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGuiMailNhacNho",
                schema: "tempplate",
                table: "NodeSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGuiMailPhongBanTiepNhan",
                schema: "tempplate",
                table: "NodeSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaoTaskBaoCao",
                schema: "tempplate",
                table: "NodeSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NoodeId",
                schema: "tempplate",
                table: "NodeSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NodeSettings",
                schema: "tempplate",
                table: "NodeSettings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NhanSuTiepNhanNodes",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoaiNhanSu = table.Column<int>(type: "int", nullable: false),
                    NodeSettingId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanSuTiepNhanNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanSuTiepNhanNodes_NodeSettings_NodeSettingId",
                        column: x => x.NodeSettingId,
                        principalSchema: "tempplate",
                        principalTable: "NodeSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhongBanTiepNhanNodes",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhongbanId = table.Column<int>(type: "int", nullable: false),
                    TenPhongBan = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoaiPhongBan = table.Column<int>(type: "int", nullable: false),
                    NodeSettingId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBanTiepNhanNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhongBanTiepNhanNodes_NodeSettings_NodeSettingId",
                        column: x => x.NodeSettingId,
                        principalSchema: "tempplate",
                        principalTable: "NodeSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanSuTiepNhanNodes_NodeSettingId",
                schema: "tempplate",
                table: "NhanSuTiepNhanNodes",
                column: "NodeSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanTiepNhanNodes_NodeSettingId",
                schema: "tempplate",
                table: "PhongBanTiepNhanNodes",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_NodeSettings_NodeSettingId",
                schema: "tempplate",
                table: "Nodes");

            migrationBuilder.DropTable(
                name: "NhanSuTiepNhanNodes",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "PhongBanTiepNhanNodes",
                schema: "tempplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NodeSettings",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.DropColumn(
                name: "CauHinhMailNhacNho",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.DropColumn(
                name: "IsGuiMailNhacNho",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.DropColumn(
                name: "IsGuiMailPhongBanTiepNhan",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.DropColumn(
                name: "IsTaoTaskBaoCao",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.DropColumn(
                name: "NoodeId",
                schema: "tempplate",
                table: "NodeSettings");

            migrationBuilder.RenameTable(
                name: "NodeSettings",
                schema: "tempplate",
                newName: "NodeSetting",
                newSchema: "tempplate");

            migrationBuilder.AlterColumn<string>(
                name: "PhongbanId",
                schema: "tempplate",
                table: "PhongBanTheoDoiQuyTrinhs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NodeSetting",
                schema: "tempplate",
                table: "NodeSetting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_NodeSetting_NodeSettingId",
                schema: "tempplate",
                table: "Nodes",
                column: "NodeSettingId",
                principalSchema: "tempplate",
                principalTable: "NodeSetting",
                principalColumn: "Id");
        }
    }
}
