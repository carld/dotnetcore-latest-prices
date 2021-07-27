using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace latest_prices.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LatestPrices",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false),
                    published_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ticker = table.Column<string>(type: "TEXT", nullable: true),
                    price_in_cents = table.Column<int>(type: "INTEGER", nullable: false),
                    Trades = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    published_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ticker = table.Column<string>(type: "TEXT", nullable: true),
                    price_in_cents = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prices_published_at",
                table: "Prices",
                column: "published_at");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ticker",
                table: "Prices",
                column: "ticker");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LatestPrices");

            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
