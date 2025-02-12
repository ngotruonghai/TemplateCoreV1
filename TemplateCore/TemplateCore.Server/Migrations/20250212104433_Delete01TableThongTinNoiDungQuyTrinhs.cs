using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateCore.Server.Migrations
{
    /// <inheritdoc />
    public partial class Delete01TableThongTinNoiDungQuyTrinhs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThongTinNoiDungQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThongTinNoiDungQuyTrinhId",
                schema: "tempplate",
                table: "NhanSuTheoDoiQuyTrinhs",
                type: "int",
                nullable: true);
        }
    }
}
