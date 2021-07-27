using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class Document : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Copy",
                table: "Document");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "Document",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Document",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Document");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Document",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte[]>(
                name: "Copy",
                table: "Document",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "Id");
        }
    }
}
