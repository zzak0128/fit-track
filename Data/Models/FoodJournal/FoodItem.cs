namespace FitTrack.Data.Models.FoodJournal;

public class FoodItem
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public double ServingSize { get; set; }

    public string Units { get; set; } = "";

    public double Calories { get; set; }

    public double Carbs { get; set; }

    public double Fats { get; set; }

    public double Proteins { get; set; }

    // Nav props
    public virtual List<MealFoodServing> MealFoodServings { get; set; } = [];
}
