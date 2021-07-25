using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class Admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AdministratorOfficer");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AdministratorOfficer");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AdministratorOfficer",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AdministratorOfficer");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "AdministratorOfficer",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "AdministratorOfficer",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
