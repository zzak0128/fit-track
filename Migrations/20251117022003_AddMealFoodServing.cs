using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddMealFoodServing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItemMeal");

            migrationBuilder.CreateTable(
                name: "MealFoodServings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FoodItemId = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<double>(type: "double", nullable: false),
                    Unit = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealFoodServings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealFoodServings_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MealMealFoodServing",
                columns: table => new
                {
                    FoodsId = table.Column<int>(type: "int", nullable: false),
                    MealsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealMealFoodServing", x => new { x.FoodsId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_MealMealFoodServing_MealFoodServings_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "MealFoodServings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealMealFoodServing_Meals_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MealFoodServings_FoodItemId",
                table: "MealFoodServings",
                column: "FoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MealMealFoodServing_MealsId",
                table: "MealMealFoodServing",
                column: "MealsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealMealFoodServing");

            migrationBuilder.DropTable(
                name: "MealFoodServings");

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
    }
}
