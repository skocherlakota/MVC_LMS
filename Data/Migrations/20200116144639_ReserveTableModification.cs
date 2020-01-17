using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_LMS.Data.Migrations
{
    public partial class ReserveTableModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_AspNetUsers_UserProfileId",
                table: "Reserves");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_UserProfileId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Reserves",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Reserves");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileId",
                table: "Reserves",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_UserProfileId",
                table: "Reserves",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_AspNetUsers_UserProfileId",
                table: "Reserves",
                column: "UserProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
