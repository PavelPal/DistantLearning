using Microsoft.EntityFrameworkCore.Migrations;

namespace distantlearning.Migrations
{
    public partial class AddedIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Email",
                "PendingUserData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "Phone",
                "PendingUserData",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Documents",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_Groups_Id",
                "Groups",
                "Id");

            migrationBuilder.CreateIndex(
                "IX_Documents_Name",
                "Documents",
                "Name");

            migrationBuilder.CreateIndex(
                "IX_Disciplines_Id",
                "Disciplines",
                "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_Groups_Id",
                "Groups");

            migrationBuilder.DropIndex(
                "IX_Documents_Name",
                "Documents");

            migrationBuilder.DropIndex(
                "IX_Disciplines_Id",
                "Disciplines");

            migrationBuilder.DropColumn(
                "Email",
                "PendingUserData");

            migrationBuilder.DropColumn(
                "Phone",
                "PendingUserData");

            migrationBuilder.AlterColumn<string>(
                "Name",
                "Documents",
                nullable: true);
        }
    }
}