using Microsoft.EntityFrameworkCore.Migrations;

namespace distantlearning.Migrations
{
    public partial class ApprovingUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                "IsApproved",
                "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                "IsPendingData",
                "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsApproved",
                "AspNetUsers");

            migrationBuilder.DropColumn(
                "IsPendingData",
                "AspNetUsers");
        }
    }
}