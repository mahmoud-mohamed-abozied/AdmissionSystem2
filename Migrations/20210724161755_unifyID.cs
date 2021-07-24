using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class unifyID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankElahly_Payment_PaymentId",
                table: "BankElahly");

            migrationBuilder.DropForeignKey(
                name: "FK_Fawry_Payment_PaymentId",
                table: "Fawry");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterCard_Payment_PaymentId",
                table: "MasterCard");

            migrationBuilder.RenameColumn(
                name: "SibilingId",
                table: "Sibling",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Payment",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MedicalHistoryId",
                table: "MedicalHistory",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "MasterCard",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Fawry",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "BankElahly",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BankElahly_Payment_Id",
                table: "BankElahly",
                column: "Id",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fawry_Payment_Id",
                table: "Fawry",
                column: "Id",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterCard_Payment_Id",
                table: "MasterCard",
                column: "Id",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankElahly_Payment_Id",
                table: "BankElahly");

            migrationBuilder.DropForeignKey(
                name: "FK_Fawry_Payment_Id",
                table: "Fawry");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterCard_Payment_Id",
                table: "MasterCard");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sibling",
                newName: "SibilingId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Payment",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MedicalHistory",
                newName: "MedicalHistoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MasterCard",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Fawry",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BankElahly",
                newName: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankElahly_Payment_PaymentId",
                table: "BankElahly",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fawry_Payment_PaymentId",
                table: "Fawry",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterCard_Payment_PaymentId",
                table: "MasterCard",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
