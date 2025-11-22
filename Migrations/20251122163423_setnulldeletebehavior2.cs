using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class setnulldeletebehavior2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealFoodServings_FoodItems_FoodItemId",
                table: "MealFoodServings");

            migrationBuilder.AddForeignKey(
                name: "FK_MealFoodServings_FoodItems_FoodItemId",
                table: "MealFoodServings",
                column: "FoodItemId",
                principalTable: "FoodItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealFoodServings_FoodItems_FoodItemId",
                table: "MealFoodServings");

            migrationBuilder.AddForeignKey(
                name: "FK_MealFoodServings_FoodItems_FoodItemId",
                table: "MealFoodServings",
                column: "FoodItemId",
                principalTable: "FoodItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
