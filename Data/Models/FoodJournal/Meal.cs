namespace FitTrack.Data.Models.FoodJournal;

public class Meal
{
    public int Id { get; set; }

    public MealType MealType { get; set; }

    public List<FoodItem> Foods { get; set; } = [];

    public DateTime Date { get; set; }


    public ApplicationUser User { get; set; } = null!;

}
