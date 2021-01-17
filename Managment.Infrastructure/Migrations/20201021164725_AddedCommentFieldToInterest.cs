using Microsoft.EntityFrameworkCore.Migrations;

namespace Managment.Infrastructure.Migrations
{
    public partial class AddedCommentFieldToInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Interests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Interests");
        }
    }
}
