using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BakingAppDataLayer.Migrations
{
    public partial class UserTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId1",
                table: "Accounts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_UserId1",
                table: "Accounts",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_UserId1",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId1",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Accounts");
        }
    }
}
