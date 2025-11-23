namespace FitTrack.Data.DTOs.FoodJournal;

public class MealFoodServingDto
{
    public int Id { get; set; }

    public FoodItemDto FoodItem { get; set; } = null!;

    public double Servings { get; set; } = 1;
}
