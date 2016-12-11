using Microsoft.EntityFrameworkCore.Migrations;

namespace distantlearning.Migrations
{
    public partial class ChangeUsersImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Photo",
                "AspNetUsers");

            migrationBuilder.RenameColumn(
                "PhotoType",
                "AspNetUsers",
                "PhotoPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "PhotoPath",
                "AspNetUsers",
                "PhotoType");

            migrationBuilder.AddColumn<byte[]>(
                "Photo",
                "AspNetUsers",
                nullable: true);
        }
    }
}