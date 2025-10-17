using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class exerciseImageFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseImages_Exercises_ExerciseId",
                table: "ExerciseImages");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseImages_Exercises_ExerciseId",
                table: "ExerciseImages",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseImages_Exercises_ExerciseId",
                table: "ExerciseImages");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseImages_Exercises_ExerciseId",
                table: "ExerciseImages",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");
        }
    }
}
