using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Managment.Infrastructure.Migrations
{
    public partial class AddedVoluteer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "Notes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    TelephoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_VolunteerId",
                table: "Notes",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Volunteers_VolunteerId",
                table: "Notes",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Volunteers_VolunteerId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Notes_VolunteerId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "Notes");
        }
    }
}
