#nullable disable

namespace Broooms.Catalog.Infrastructure.Data.EntityFramework.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddCategoryAndProduct : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table =>
                new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(255)",
                        maxLength: 255,
                        nullable: false
                    ),
                    Description = table.Column<string>(
                        type: "character varying(1024)",
                        maxLength: 1024,
                        nullable: false
                    ),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    UpdatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    )
                },
            constraints: table => table.PrimaryKey("PK_Categories", x => x.Id)
        );

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table =>
                new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(255)",
                        maxLength: 255,
                        nullable: false
                    ),
                    Description = table.Column<string>(
                        type: "character varying(1024)",
                        maxLength: 1024,
                        nullable: false
                    ),
                    PriceInCents = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(
                        type: "character varying(512)",
                        maxLength: 512,
                        nullable: false
                    ),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    UpdatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    )
                },
            constraints: table => table.PrimaryKey("PK_Products", x => x.Id)
        );

        migrationBuilder.CreateTable(
            name: "ProductCategory",
            columns: table =>
                new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
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
            name: "IX_Categories_Name",
            table: "Categories",
            column: "Name",
            unique: true
        );

        migrationBuilder.CreateIndex(
            name: "IX_ProductCategory_ProductsId",
            table: "ProductCategory",
            column: "ProductsId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_Products_Name",
            table: "Products",
            column: "Name",
            unique: true
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ProductCategory");

        migrationBuilder.DropTable(name: "Categories");

        migrationBuilder.DropTable(name: "Products");
    }
}
