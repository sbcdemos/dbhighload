using Microsoft.EntityFrameworkCore.Migrations;

namespace mysqlproject.Migrations
{
    public partial class fields_to_sales_added2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sales_TheDate",
                table: "Sales",
                column: "TheDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sales_TheDate",
                table: "Sales");
        }
    }
}
