using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class addFamilyStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchoolName",
                table: "Sibling",
                newName: "SchoolBranch");

            migrationBuilder.CreateTable(
                name: "FamilyStatues",
                columns: table => new
                {
                    Guardian = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GuardianAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageSpoken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyStatues", x => x.Guardian);
                    table.ForeignKey(
                        name: "FK_FamilyStatues_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FamilyStatues_ApplicantId",
                table: "FamilyStatues",
                column: "ApplicantId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyStatues");

            migrationBuilder.RenameColumn(
                name: "SchoolBranch",
                table: "Sibling",
                newName: "SchoolName");
        }
    }
}
