using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class AddDatabaseupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InterviewTime",
                table: "InterviewCriteria",
                newName: "InterviewDuration");

            migrationBuilder.RenameColumn(
                name: "Relegion",
                table: "Applicant",
                newName: "Religion");

            migrationBuilder.RenameColumn(
                name: "FamilyStatus",
                table: "Applicant",
                newName: "AdmissionDate");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Sibling",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "ParentInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterviewDate",
                table: "Interview",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterviewTime",
                table: "Interview",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "Interview",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ScoreGrade",
                table: "Interview",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "EmergencyContact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Document",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MedicalHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_AdmissionDetails_AdmissionDetailsId",
                        column: x => x.AdmissionDetailsId,
                        principalTable: "AdmissionDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_Applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicant",
                        principalColumn: "ApplicantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Application_MedicalHistory_MedicalHistoryId",
                        column: x => x.MedicalHistoryId,
                        principalTable: "MedicalHistory",
                        principalColumn: "MedicalHistoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Application_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sibling_ApplicationId",
                table: "Sibling",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentInfo_ApplicationId",
                table: "ParentInfo",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContact_ApplicationId",
                table: "EmergencyContact",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_ApplicationId",
                table: "Document",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_AdmissionDetailsId",
                table: "Application",
                column: "AdmissionDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_ApplicantId",
                table: "Application",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_MedicalHistoryId",
                table: "Application",
                column: "MedicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_PaymentId",
                table: "Application",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Application_ApplicationId",
                table: "Document",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyContact_Application_ApplicationId",
                table: "EmergencyContact",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentInfo_Application_ApplicationId",
                table: "ParentInfo",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sibling_Application_ApplicationId",
                table: "Sibling",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Application_ApplicationId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyContact_Application_ApplicationId",
                table: "EmergencyContact");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentInfo_Application_ApplicationId",
                table: "ParentInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Sibling_Application_ApplicationId",
                table: "Sibling");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropIndex(
                name: "IX_Sibling_ApplicationId",
                table: "Sibling");

            migrationBuilder.DropIndex(
                name: "IX_ParentInfo_ApplicationId",
                table: "ParentInfo");

            migrationBuilder.DropIndex(
                name: "IX_EmergencyContact_ApplicationId",
                table: "EmergencyContact");

            migrationBuilder.DropIndex(
                name: "IX_Document_ApplicationId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Sibling");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "ParentInfo");

            migrationBuilder.DropColumn(
                name: "InterviewDate",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "InterviewTime",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "ScoreGrade",
                table: "Interview");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "EmergencyContact");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Document");

            migrationBuilder.RenameColumn(
                name: "InterviewDuration",
                table: "InterviewCriteria",
                newName: "InterviewTime");

            migrationBuilder.RenameColumn(
                name: "Religion",
                table: "Applicant",
                newName: "Relegion");

            migrationBuilder.RenameColumn(
                name: "AdmissionDate",
                table: "Applicant",
                newName: "FamilyStatus");
        }
    }
}
