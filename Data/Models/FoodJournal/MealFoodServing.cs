namespace FitTrack.Data.Models.FoodJournal;

public class MealFoodServing
{
    public int Id { get; set; }

    public FoodItem FoodItem { get; set; } = null!;

    public double Servings { get; set; }

    // Navigaiton Props
    public virtual List<Meal> Meals { get; set; } = [];
}
