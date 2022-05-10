using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIAttendanceSystem.Migrations
{
    public partial class staff_department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeptName",
                table: "BuildingAffairsStaffs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeptName",
                table: "BuildingAffairsStaffs");
        }
    }
}
