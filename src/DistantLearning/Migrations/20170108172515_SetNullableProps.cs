using Microsoft.EntityFrameworkCore.Migrations;

namespace distantlearning.Migrations
{
    public partial class SetNullableProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Answers_Questions_QuestionId",
                "Answers");

            migrationBuilder.DropForeignKey(
                "FK_Questions_Tests_TestId",
                "Questions");

            migrationBuilder.AlterColumn<int>(
                "TestId",
                "Questions",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                "QuestionId",
                "Answers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                "FK_Answers_Questions_QuestionId",
                "Answers",
                "QuestionId",
                "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Questions_Tests_TestId",
                "Questions",
                "TestId",
                "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Answers_Questions_QuestionId",
                "Answers");

            migrationBuilder.DropForeignKey(
                "FK_Questions_Tests_TestId",
                "Questions");

            migrationBuilder.AlterColumn<int>(
                "TestId",
                "Questions",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                "QuestionId",
                "Answers",
                nullable: false);

            migrationBuilder.AddForeignKey(
                "FK_Answers_Questions_QuestionId",
                "Answers",
                "QuestionId",
                "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Questions_Tests_TestId",
                "Questions",
                "TestId",
                "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}