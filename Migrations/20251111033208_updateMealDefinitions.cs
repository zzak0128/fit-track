using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class updateMealDefinitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodItems_Meals_MealId",
                table: "FoodItems");

            migrationBuilder.DropIndex(
                name: "IX_FoodItems_MealId",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "FoodItems");

            migrationBuilder.CreateTable(
                name: "FoodItemMeal",
                columns: table => new
                {
                    FoodsId = table.Column<int>(type: "int", nullable: false),
                    MealsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemMeal", x => new { x.FoodsId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_FoodItemMeal_FoodItems_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItemMeal_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemMeal_MealsId",
                table: "FoodItemMeal",
                column: "MealsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItemMeal");

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "FoodItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FoodItems_MealId",
                table: "FoodItems",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodItems_Meals_MealId",
                table: "FoodItems",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
