using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class AddDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copy",
                table: "Document");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Copy",
                table: "Document",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
