using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class CreateTrendTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrendCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TrendTime",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Trend",
                columns: table => new
                {
                    TrendId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrendCount = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trend", x => x.TrendId);
                    table.ForeignKey(
                        name: "FK_Trend_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trend_ProductId",
                table: "Trend",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trend");

            migrationBuilder.AddColumn<int>(
                name: "TrendCount",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TrendTime",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }
    }
}
