#nullable disable

namespace Broooms.Catalog.Data.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

public partial class AddProductImageName : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder) =>
        migrationBuilder.AddColumn<string>(
            name: "ImageName",
            table: "Products",
            type: "character varying(100)",
            maxLength: 100,
            nullable: true
        );

    protected override void Down(MigrationBuilder migrationBuilder) =>
        migrationBuilder.DropColumn(name: "ImageName", table: "Products");
}
