using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIAttendanceSystem.Migrations
{
    public partial class student_section : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecNo",
                table: "Students",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecNo",
                table: "Students");
        }
    }
}
