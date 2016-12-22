using Microsoft.EntityFrameworkCore.Migrations;

namespace distantlearning.Migrations
{
    public partial class ChangedGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Name",
                "Groups",
                "Postfix");

            migrationBuilder.AddColumn<int>(
                "Prefix",
                "Groups",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Prefix",
                "Groups");

            migrationBuilder.RenameColumn(
                "Postfix",
                "Groups",
                "Name");
        }
    }
}