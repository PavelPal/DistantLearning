using Microsoft.EntityFrameworkCore.Migrations;

namespace distantlearning.Migrations
{
    public partial class ChangedModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_TestResults_Tests_TestId",
                "TestResults");

            migrationBuilder.AlterColumn<int>(
                "TestId",
                "TestResults",
                nullable: true);

            migrationBuilder.AddForeignKey(
                "FK_TestResults_Tests_TestId",
                "TestResults",
                "TestId",
                "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_TestResults_Tests_TestId",
                "TestResults");

            migrationBuilder.AlterColumn<int>(
                "TestId",
                "TestResults",
                nullable: false);

            migrationBuilder.AddForeignKey(
                "FK_TestResults_Tests_TestId",
                "TestResults",
                "TestId",
                "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}