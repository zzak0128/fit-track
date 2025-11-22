namespace FitTrack.Data.DTOs.FoodJournal;

public class UserGoalsDto
{
    public int Id { get; set; }

    public int Calories { get; set; }

    public int Carbs { get; set; }

    public int Fats { get; set; }

    public int Protein { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
