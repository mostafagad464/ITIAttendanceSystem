using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITIAttendanceSystem.Migrations
{
    public partial class student_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Students");
        }
    }
}
