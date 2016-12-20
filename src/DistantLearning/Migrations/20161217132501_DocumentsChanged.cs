using Microsoft.EntityFrameworkCore.Migrations;

namespace distantlearning.Migrations
{
    public partial class DocumentsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "File",
                "Documents");

            migrationBuilder.RenameColumn(
                "FileType",
                "Documents",
                "FileCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "FileCode",
                "Documents",
                "FileType");

            migrationBuilder.AddColumn<byte[]>(
                "File",
                "Documents",
                nullable: true);
        }
    }
}