using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class AddId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilyStatues",
                table: "FamilyStatues");

            migrationBuilder.AlterColumn<string>(
                name: "Guardian",
                table: "FamilyStatues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "FamilyStatues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilyStatues",
                table: "FamilyStatues",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FamilyStatues",
                table: "FamilyStatues");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FamilyStatues");

            migrationBuilder.AlterColumn<string>(
                name: "Guardian",
                table: "FamilyStatues",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamilyStatues",
                table: "FamilyStatues",
                column: "Guardian");
        }
    }
}
