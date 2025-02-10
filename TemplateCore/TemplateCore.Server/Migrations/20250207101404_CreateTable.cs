using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tempplate");

            migrationBuilder.CreateTable(
                name: "DanhMucs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UrlLink = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhSachQuyTrinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaQuyTrinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThietLapMaPhieu = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TenQuyTrinh = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhSachQuyTrinhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyActives",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyACtiveCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TypeKey = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyActives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NodeSettings",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GhiChu = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    NoodeId = table.Column<int>(type: "int", nullable: true),
                    CauHinhMailNhacNho = table.Column<int>(type: "int", nullable: true),
                    IsTaoTaskBaoCao = table.Column<bool>(type: "bit", nullable: false),
                    IsGuiMailPhongBanTiepNhan = table.Column<bool>(type: "bit", nullable: false),
                    IsGuiMailNhacNho = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhongBans",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhongBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenPhongBan = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinNoiDungQuyTrinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThongTin = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CauHinhHienThiBuoc = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    BatBuocNhap = table.Column<bool>(type: "bit", nullable: false),
                    FileDinhKem = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinNoiDungQuyTrinhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucPhanQuyens",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DanhMucId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DanhMucId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucPhanQuyens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhMucPhanQuyens_DanhMucs_DanhMucId1",
                        column: x => x.DanhMucId1,
                        principalSchema: "tempplate",
                        principalTable: "DanhMucs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiagramNodes",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Target = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TenDiagram = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
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
                    table.PrimaryKey("PK_DiagramNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagramNodes_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                        column: x => x.DanhSachQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "PhongBanTheoDoiQuyTrinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhongbanId = table.Column<int>(type: "int", nullable: false),
                    TenPhongBan = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoaiPhongBan = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_PhongBanTheoDoiQuyTrinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhongBanTheoDoiQuyTrinhs_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                        column: x => x.DanhSachQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Nodes",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    TenNode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    X = table.Column<int>(type: "int", nullable: true),
                    Y = table.Column<int>(type: "int", nullable: true),
                    DanhSachQuyTrinhId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nodes_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                        column: x => x.DanhSachQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nodes_NodeSettings_NodeSettingId",
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

            migrationBuilder.CreateTable(
                name: "NhanSuTheoDoiQuyTrinhs",
                schema: "tempplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LoaiNhanSu = table.Column<int>(type: "int", nullable: false),
                    DanhSachQuyTrinhId = table.Column<int>(type: "int", nullable: true),
                    ThongTinNoiDungQuyTrinhId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserParentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanSuTheoDoiQuyTrinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NhanSuTheoDoiQuyTrinhs_DanhSachQuyTrinhs_DanhSachQuyTrinhId",
                        column: x => x.DanhSachQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "DanhSachQuyTrinhs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NhanSuTheoDoiQuyTrinhs_ThongTinNoiDungQuyTrinhs_ThongTinNoiDungQuyTrinhId",
                        column: x => x.ThongTinNoiDungQuyTrinhId,
                        principalSchema: "tempplate",
                        principalTable: "ThongTinNoiDungQuyTrinhs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhMucPhanQuyens_DanhMucId1",
                schema: "tempplate",
                table: "DanhMucPhanQuyens",
                column: "DanhMucId1");

            migrationBuilder.CreateIndex(
                name: "IX_DiagramNodes_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "DiagramNodes",
                column: "DanhSachQuyTrinhId");

            migrationBuilder.CreateIndex(
                name: "IX_NextStep_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "NextStep",
                column: "DanhSachQuyTrinhId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSuTheoDoiQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs",
                column: "DanhSachQuyTrinhId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSuTheoDoiQuyTrinhs_ThongTinNoiDungQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs",
                column: "ThongTinNoiDungQuyTrinhId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanSuTiepNhanNodes_NodeSettingId",
                schema: "tempplate",
                table: "NhanSuTiepNhanNodes",
                column: "NodeSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "Nodes",
                column: "DanhSachQuyTrinhId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_NodeSettingId",
                schema: "tempplate",
                table: "Nodes",
                column: "NodeSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanTheoDoiQuyTrinhs_DanhSachQuyTrinhId",
                schema: "tempplate",
                table: "PhongBanTheoDoiQuyTrinhs",
                column: "DanhSachQuyTrinhId");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBanTiepNhanNodes_NodeSettingId",
                schema: "tempplate",
                table: "PhongBanTiepNhanNodes",
                column: "NodeSettingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DanhMucPhanQuyens",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "DiagramNodes",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "KeyActives",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "NextStep",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "NhanSuTheoDoiQuyTrinhs",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "NhanSuTiepNhanNodes",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "Nodes",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "PhongBans",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "PhongBanTheoDoiQuyTrinhs",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "PhongBanTiepNhanNodes",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "DanhMucs",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "ThongTinNoiDungQuyTrinhs",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "DanhSachQuyTrinhs",
                schema: "tempplate");

            migrationBuilder.DropTable(
                name: "NodeSettings",
                schema: "tempplate");
        }
    }
}
