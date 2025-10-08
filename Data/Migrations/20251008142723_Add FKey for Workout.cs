using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddFKeyforWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Routines_RoutineId1",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_RoutineId1",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "RoutineId1",
                table: "Workouts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoutineId1",
                table: "Workouts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_RoutineId1",
                table: "Workouts",
                column: "RoutineId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Routines_RoutineId1",
                table: "Workouts",
                column: "RoutineId1",
                principalTable: "Routines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
