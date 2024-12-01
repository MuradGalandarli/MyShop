using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class relationUserFeedbackScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FeedbackScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackScores_UserId",
                table: "FeedbackScores",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackScores_Users_UserId",
                table: "FeedbackScores",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackScores_Users_UserId",
                table: "FeedbackScores");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackScores_UserId",
                table: "FeedbackScores");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FeedbackScores");
        }
    }
}
