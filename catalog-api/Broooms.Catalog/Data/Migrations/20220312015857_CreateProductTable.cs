#nullable disable

namespace Broooms.Catalog.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

public partial class CreateProductTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder) =>
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table =>
                new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    Description = table.Column<string>(
                        type: "character varying(1000)",
                        maxLength: 1000,
                        nullable: false
                    ),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(
                        type: "character varying(300)",
                        maxLength: 300,
                        nullable: true
                    )
                },
            constraints: table => table.PrimaryKey("PK_Products", x => x.Id)
        );

    protected override void Down(MigrationBuilder migrationBuilder) =>
        migrationBuilder.DropTable(name: "Products");
}
