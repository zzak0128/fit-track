using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class activitesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Workouts_WorkoutId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutLogs_AspNetUsers_UserId",
                table: "WorkoutLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutLogs_Workouts_WorkoutId",
                table: "WorkoutLogs");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutLogs_UserId",
                table: "WorkoutLogs");

            migrationBuilder.DropIndex(
                name: "IX_Activities_WorkoutId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkoutLogs");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "WorkoutId",
                table: "WorkoutLogs",
                newName: "ExerciseId");

            migrationBuilder.RenameColumn(
                name: "TimeElapsedSeconds",
                table: "WorkoutLogs",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutLogs_WorkoutId",
                table: "WorkoutLogs",
                newName: "IX_WorkoutLogs_ExerciseId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCompleted",
                table: "WorkoutLogs",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_ActivityId",
                table: "WorkoutLogs",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutLogs_Activities_ActivityId",
                table: "WorkoutLogs",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutLogs_Exercises_ExerciseId",
                table: "WorkoutLogs",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutLogs_Activities_ActivityId",
                table: "WorkoutLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutLogs_Exercises_ExerciseId",
                table: "WorkoutLogs");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutLogs_ActivityId",
                table: "WorkoutLogs");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "WorkoutLogs",
                newName: "WorkoutId");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "WorkoutLogs",
                newName: "TimeElapsedSeconds");

            migrationBuilder.RenameIndex(
                name: "IX_WorkoutLogs_ExerciseId",
                table: "WorkoutLogs",
                newName: "IX_WorkoutLogs_WorkoutId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCompleted",
                table: "WorkoutLogs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WorkoutLogs",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Activities",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Activities",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_UserId",
                table: "WorkoutLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_WorkoutId",
                table: "Activities",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Workouts_WorkoutId",
                table: "Activities",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutLogs_AspNetUsers_UserId",
                table: "WorkoutLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutLogs_Workouts_WorkoutId",
                table: "WorkoutLogs",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
