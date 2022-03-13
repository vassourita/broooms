#nullable disable

namespace Broooms.Catalog.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

public partial class FixedProductCategoriesTableName : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder) =>
        migrationBuilder.DropTable(name: "ProductCategory");

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ProductCategory",
            columns: table =>
                new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductCategory", x => new { x.CategoriesId, x.ProductsId });
                table.ForeignKey(
                    name: "FK_ProductCategory_Categories_CategoriesId",
                    column: x => x.CategoriesId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    name: "FK_ProductCategory_Products_ProductsId",
                    column: x => x.ProductsId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateIndex(
            name: "IX_ProductCategory_ProductsId",
            table: "ProductCategory",
            column: "ProductsId"
        );
    }
}
