using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    public partial class AddedArticlesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "BasketId", "Description", "Price", "ProductCategory" },
                values: new object[] { 1, null, "tomatoes", 10, 2 });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "BasketId", "Description", "Price", "ProductCategory" },
                values: new object[] { 2, null, "apples", 6, 2 });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "BasketId", "Description", "Price", "ProductCategory" },
                values: new object[] { 3, null, "hammer", 8, 4 });

            migrationBuilder.InsertData(
                table: "Article",
                columns: new[] { "Id", "BasketId", "Description", "Price", "ProductCategory" },
                values: new object[] { 4, null, "lego", 20, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Article",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
