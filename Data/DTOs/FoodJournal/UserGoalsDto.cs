namespace FitTrack.Data.DTOs.FoodJournal;

public class UserGoalsDto
{
    public int Id { get; set; }

    public double Calories { get; set; }

    public double Carbs { get; set; }

    public double Fats { get; set; }

    public double Protein { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
