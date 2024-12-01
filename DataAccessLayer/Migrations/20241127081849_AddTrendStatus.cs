using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddTrendStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trend_Products_ProductId",
                table: "Trend");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trend",
                table: "Trend");

            migrationBuilder.RenameTable(
                name: "Trend",
                newName: "Trends");

            migrationBuilder.RenameIndex(
                name: "IX_Trend_ProductId",
                table: "Trends",
                newName: "IX_Trends_ProductId");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Trends",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trends",
                table: "Trends",
                column: "TrendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trends_Products_ProductId",
                table: "Trends",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trends_Products_ProductId",
                table: "Trends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trends",
                table: "Trends");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Trends");

            migrationBuilder.RenameTable(
                name: "Trends",
                newName: "Trend");

            migrationBuilder.RenameIndex(
                name: "IX_Trends_ProductId",
                table: "Trend",
                newName: "IX_Trend_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trend",
                table: "Trend",
                column: "TrendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trend_Products_ProductId",
                table: "Trend",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
