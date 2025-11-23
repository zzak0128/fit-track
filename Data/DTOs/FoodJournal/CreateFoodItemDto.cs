namespace FitTrack.Data.DTOs.FoodJournal;

public class CreateFoodItemDto
{
    public string Name { get; set; } = "";

    public double ServingSize { get; set; }

    public string Units { get; set; } = "Each";

    public double Calories { get; set; }

    public double Carbs { get; set; }

    public double Fats { get; set; }

    public double Proteins { get; set; }
}
