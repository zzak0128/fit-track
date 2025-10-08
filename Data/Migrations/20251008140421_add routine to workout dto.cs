using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class addroutinetoworkoutdto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoutineId",
                table: "Workouts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "RoutineId",
                table: "Workouts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
