using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace distantlearning.Migrations
{
    public partial class PendingUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                "LastName",
                "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                "FirstName",
                "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                "PendingUserData",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingUserData", x => x.Id);
                    table.ForeignKey(
                        "FK_PendingUserData_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_FirstName",
                "AspNetUsers",
                "FirstName");

            migrationBuilder.CreateIndex(
                "IX_AspNetUsers_LastName",
                "AspNetUsers",
                "LastName");

            migrationBuilder.CreateIndex(
                "IX_PendingUserData_UserId",
                "PendingUserData",
                "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PendingUserData");

            migrationBuilder.DropIndex(
                "IX_AspNetUsers_FirstName",
                "AspNetUsers");

            migrationBuilder.DropIndex(
                "IX_AspNetUsers_LastName",
                "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                "LastName",
                "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                "FirstName",
                "AspNetUsers",
                nullable: true);
        }
    }
}