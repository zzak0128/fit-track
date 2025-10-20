using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddSetCounttoWorkoutLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SetCount",
                table: "WorkoutLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetCount",
                table: "WorkoutLogs");
        }
    }
}
