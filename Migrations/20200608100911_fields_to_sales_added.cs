using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mysqlproject.Migrations
{
    public partial class fields_to_sales_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TheDate",
                table: "Sales",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TheDate",
                table: "Sales");
        }
    }
}
