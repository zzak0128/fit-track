namespace FitTrack.Data.Models.FoodJournal;

public class FoodItem
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public double ServingSize { get; set; }

    public string Units { get; set; } = "";

    public int Calories { get; set; }

    public int Carbs { get; set; }

    public int Fats { get; set; }

    public int Proteins { get; set; }

    // Nav props
    public virtual List<Meal> Meals { get; set; } = [];
}
