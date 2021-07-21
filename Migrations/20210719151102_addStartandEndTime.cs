using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class addStartandEndTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfInterviewer",
                table: "InterviewCriteria",
                newName: "NumberOfInterviewers");

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "InterviewCriteria",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "InterviewCriteria",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "InterviewCriteria");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "InterviewCriteria");

            migrationBuilder.RenameColumn(
                name: "NumberOfInterviewers",
                table: "InterviewCriteria",
                newName: "NumberOfInterviewer");
        }
    }
}
