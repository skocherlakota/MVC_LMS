using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_LMS.Data.Migrations
{
    public partial class ReserveTableCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Reserves",
                columns: table => new
                {
                    ReserveID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<string>(nullable: true),
                    BookID = table.Column<int>(nullable: false),
                    ReserveDate = table.Column<DateTime>(nullable: false),
                    UserLastUpdated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    LogicalDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves", x => x.ReserveID);
                    table.ForeignKey(
                        name: "FK_Reserves_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserves_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_BookID",
                table: "Reserves",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_UserProfileId",
                table: "Reserves",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserves");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
