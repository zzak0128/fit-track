namespace FitTrack.Data.DTOs.Activities;

public class CreateActivityDto
{
    public int WorkoutId { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
