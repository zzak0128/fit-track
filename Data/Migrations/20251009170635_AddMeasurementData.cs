using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddMeasurementData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Measurements");

            migrationBuilder.RenameColumn(
                name: "Metric",
                table: "Measurements",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "MeasurementData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MeasurementId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementData_Measurements_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementData_MeasurementId",
                table: "MeasurementData",
                column: "MeasurementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasurementData");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Measurements",
                newName: "Metric");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Measurements",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Number",
                table: "Measurements",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
