using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Managment.Infrastructure.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Capacity = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: true),
                    Gender = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    EstimatedAge = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Race = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Image = table.Column<byte[]>(nullable: true),
                    DateOfArrival = table.Column<DateTime>(nullable: false),
                    DateOfAdoption = table.Column<DateTime>(nullable: true),
                    DateOfPassing = table.Column<DateTime>(nullable: true),
                    Sterilised = table.Column<bool>(nullable: false),
                    KidFriendly = table.Column<int>(nullable: false),
                    ReasonAdoptable = table.Column<string>(nullable: false),
                    Adoptable = table.Column<bool>(nullable: false),
                    AdoptedBy = table.Column<string>(nullable: true),
                    ResidenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Residences_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(nullable: false),
                    WrittenOn = table.Column<DateTime>(nullable: false),
                    MadeBy = table.Column<string>(nullable: false),
                    AnimalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Charge = table.Column<double>(nullable: true),
                    AllowedAgeInMonths = table.Column<int>(nullable: true),
                    PerformedBy = table.Column<string>(nullable: false),
                    PerformedOn = table.Column<DateTime>(nullable: false),
                    AnimalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Residences",
                columns: new[] { "Id", "Capacity", "Gender", "Name", "Type" },
                values: new object[,]
                {
                    { 1, 4, 0, "Cage1HondMan4", 1 },
                    { 2, 4, 1, "Cage2KatVrouw4", 0 },
                    { 3, 4, 0, "Cage3KatMan4", 0 },
                    { 4, 4, 1, "Cage4HondVrouw4", 1 },
                    { 5, 1, null, "Cage5", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ResidenceId",
                table: "Animals",
                column: "ResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_AnimalId",
                table: "Notes",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_AnimalId",
                table: "Treatments",
                column: "AnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Residences");
        }
    }
}
