using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_LMS.Data.Migrations
{
    public partial class ReserveTableModification_ProperFK_UserIDv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
               name: "FK_Reserves_AspNetUsers_UserID",
               table: "Reserves",
               column: "UserID",
               principalTable: "AspNetUsers",
               principalColumn: "Id",
               onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
