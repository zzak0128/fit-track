namespace FitTrack.Data.DTOs.FoodJournal;

public class CreateFoodItemDto
{
    public string Name { get; set; } = "";

    public double ServingSize { get; set; }

    public string Units { get; set; } = "Each";

    public int Calories { get; set; }

    public int Carbs { get; set; }

    public int Fats { get; set; }

    public int Proteins { get; set; }
}
