using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddFKeyforWorkout5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Workouts_WorkoutId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSets_Workouts_WorkoutId",
                table: "ExerciseSets");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Workouts_WorkoutId",
                table: "Activities",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSets_Workouts_WorkoutId",
                table: "ExerciseSets",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Workouts_WorkoutId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSets_Workouts_WorkoutId",
                table: "ExerciseSets");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Workouts_WorkoutId",
                table: "Activities",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSets_Workouts_WorkoutId",
                table: "ExerciseSets",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
