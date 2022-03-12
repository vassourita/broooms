#nullable disable

namespace Broooms.Catalog.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

public partial class CreateCategoryTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table =>
                new
                {
                    Id = table
                        .Column<int>(type: "integer", nullable: false)
                        .Annotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.SerialColumn
                        ),
                    Name = table.Column<string>(
                        type: "character varying(40)",
                        maxLength: 40,
                        nullable: false
                    ),
                    Description = table.Column<string>(
                        type: "character varying(200)",
                        maxLength: 200,
                        nullable: false
                    )
                },
            constraints: table => table.PrimaryKey("PK_Categories", x => x.Id)
        );

        migrationBuilder.CreateTable(
            name: "ProductCategories",
            columns: table =>
                new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductCategories", x => new { x.CategoriesId, x.ProductsId });
                table.ForeignKey(
                    name: "FK_ProductCategories_Categories_CategoriesId",
                    column: x => x.CategoriesId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    name: "FK_ProductCategories_Products_ProductsId",
                    column: x => x.ProductsId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

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
            name: "IX_ProductCategories_ProductsId",
            table: "ProductCategories",
            column: "ProductsId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_ProductCategory_ProductsId",
            table: "ProductCategory",
            column: "ProductsId"
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ProductCategories");

        migrationBuilder.DropTable(name: "ProductCategory");

        migrationBuilder.DropTable(name: "Categories");
    }
}
