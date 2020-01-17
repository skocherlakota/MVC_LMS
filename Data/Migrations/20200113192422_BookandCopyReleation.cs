using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC_LMS.Data.Migrations
{
    public partial class BookandCopyReleation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Copi",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<string>(nullable: true),
                    AccessionNo = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UserLastUpdated = table.Column<string>(nullable: true),
                    DateLastUpdated = table.Column<DateTime>(nullable: false),
                    LogicalDeleted = table.Column<bool>(nullable: false),
                    IsBorrowed = table.Column<bool>(nullable: false),
                    BookID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Copi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Copi_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Copi_BookID",
                table: "Copi",
                column: "BookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Copi");
        }
    }
}
