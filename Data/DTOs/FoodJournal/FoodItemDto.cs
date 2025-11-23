namespace FitTrack.Data.DTOs.FoodJournal;

public class FoodItemDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public double ServingSize { get; set; }

    public string Units { get; set; } = "";

    public double Calories { get; set; }

    public double Carbs { get; set; }

    public double Fats { get; set; }

    public double Proteins { get; set; }
}
