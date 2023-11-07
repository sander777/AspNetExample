using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sander.TestTask.Persistance.Mssql.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "market_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    meta_data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_market_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "auction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    finished_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    seller = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    buyer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auction", x => x.id);
                    table.ForeignKey(
                        name: "FK_auction_market_items_item_id",
                        column: x => x.item_id,
                        principalTable: "market_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_auction_created_at",
                table: "auction",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_auction_item_id",
                table: "auction",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_auction_price",
                table: "auction",
                column: "price");

            migrationBuilder.CreateIndex(
                name: "IX_auction_seller",
                table: "auction",
                column: "seller");

            migrationBuilder.CreateIndex(
                name: "IX_auction_status",
                table: "auction",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_market_items_name",
                table: "market_items",
                column: "name");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auction");

            migrationBuilder.DropTable(
                name: "market_items");


        }
    }
}
