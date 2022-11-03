using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    public partial class _1008_orderCompletedEntityFluentApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompletedOrders_OrderId",
                table: "CompletedOrders");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedOrders_OrderId",
                table: "CompletedOrders",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompletedOrders_OrderId",
                table: "CompletedOrders");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedOrders_OrderId",
                table: "CompletedOrders",
                column: "OrderId");
        }
    }
}
