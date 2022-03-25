#nullable disable

namespace Broooms.Auth.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

public partial class CreateInitialEntities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Claims",
            columns: table =>
                new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(
                        type: "nvarchar(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    Description = table.Column<string>(
                        type: "nvarchar(512)",
                        maxLength: 512,
                        nullable: false
                    ),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
            constraints: table => table.PrimaryKey("PK_Claims", x => x.Id)
        );

        migrationBuilder.CreateTable(
            name: "UserAccounts",
            columns: table =>
                new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(
                        type: "nvarchar(512)",
                        maxLength: 512,
                        nullable: false
                    ),
                    FirstName = table.Column<string>(
                        type: "nvarchar(255)",
                        maxLength: 255,
                        nullable: false
                    ),
                    LastName = table.Column<string>(
                        type: "nvarchar(255)",
                        maxLength: 255,
                        nullable: false
                    ),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
            constraints: table => table.PrimaryKey("PK_UserAccounts", x => x.Id)
        );

        migrationBuilder.CreateTable(
            name: "AccessTokens",
            columns: table =>
                new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_AccessTokens", x => x.Id);
                table.ForeignKey(
                    name: "FK_AccessTokens_UserAccounts_UserId",
                    column: x => x.UserId,
                    principalTable: "UserAccounts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "RefreshTokens",
            columns: table =>
                new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                table.ForeignKey(
                    name: "FK_RefreshTokens_UserAccounts_UserId",
                    column: x => x.UserId,
                    principalTable: "UserAccounts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateTable(
            name: "UserClaims",
            columns: table =>
                new
                {
                    AccountsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserClaims", x => new { x.AccountsId, x.ClaimsId });
                table.ForeignKey(
                    name: "FK_UserClaims_Claims_ClaimsId",
                    column: x => x.ClaimsId,
                    principalTable: "Claims",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    name: "FK_UserClaims_UserAccounts_AccountsId",
                    column: x => x.AccountsId,
                    principalTable: "UserAccounts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateIndex(
            name: "IX_AccessTokens_UserId",
            table: "AccessTokens",
            column: "UserId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_RefreshTokens_UserId",
            table: "RefreshTokens",
            column: "UserId"
        );

        migrationBuilder.CreateIndex(
            name: "IX_UserClaims_ClaimsId",
            table: "UserClaims",
            column: "ClaimsId"
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "AccessTokens");

        migrationBuilder.DropTable(name: "RefreshTokens");

        migrationBuilder.DropTable(name: "UserClaims");

        migrationBuilder.DropTable(name: "Claims");

        migrationBuilder.DropTable(name: "UserAccounts");
    }
}
