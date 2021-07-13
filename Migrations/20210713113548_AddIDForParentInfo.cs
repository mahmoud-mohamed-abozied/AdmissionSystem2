using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class AddIDForParentInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentInfo",
                table: "ParentInfo");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "ParentInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ParentInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentInfo",
                table: "ParentInfo",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentInfo",
                table: "ParentInfo");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ParentInfo");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "ParentInfo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentInfo",
                table: "ParentInfo",
                column: "FirstName");
        }
    }
}
