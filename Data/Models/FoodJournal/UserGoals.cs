namespace FitTrack.Data.Models.FoodJournal;

public class UserGoals
{
    public int Id { get; set; }

    public double Calories { get; set; }

    public double Carbs { get; set; }

    public double Fats { get; set; }

    public double Protein { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
