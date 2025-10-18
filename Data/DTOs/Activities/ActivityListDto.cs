namespace FitTrack.Data.DTOs.Activities;

public class ActivityListDto
{
    public int ActivityId { get; set; }

    public string WorkoutName { get; set; } = "";

    public ApplicationUser User { get; set; }

}
