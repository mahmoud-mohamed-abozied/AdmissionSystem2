using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionSystem2.Migrations
{
    public partial class TestByte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Copy",
                table: "Document",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copy",
                table: "Document");
        }
    }
}
