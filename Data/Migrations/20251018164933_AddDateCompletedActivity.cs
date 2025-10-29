using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddDateCompletedActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "WorkoutLogs");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "Activities",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "Activities");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "WorkoutLogs",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
