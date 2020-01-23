using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MVC_LMS.Data.Migrations
{
    public partial class AddComputedColoumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "Borrows",
                nullable: true,
                oldClrType: typeof(DateTime),
                computedColumnSql: "DATEADD(day, 14, [BorrowDate]) ",
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
               name: "DueDate",
               table: "Borrows",
               type: "datetime2",
               nullable: false,
               oldClrType: typeof(DateTime),
               oldNullable: true);
        }
    }
}
