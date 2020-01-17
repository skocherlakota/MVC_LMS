using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_LMS.Data.Migrations
{
    public partial class BorrowTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    BorrowID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true,maxLength: 450),
                    CopyID = table.Column<int>(nullable: false),
                    BorrowDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    ActualReturnDate = table.Column<DateTime>(nullable: false),
                    Fine = table.Column<double>(nullable: false),
                    UserLastUpdated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    LogicalDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.BorrowID);
                    table.ForeignKey(
                        name: "FK_Borrows_Copi_CopyID",
                        column: x => x.CopyID,
                        principalTable: "Copi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Borrows_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_CopyID",
                table: "Borrows",
                column: "CopyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrows");
        }
    }
}
