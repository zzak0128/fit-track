namespace FitTrack.Data.DTOs.Activities;

public class ActivityListDto
{
    public int ActivityId { get; set; }

    public string RoutineName { get; set; } = "";

    public string WorkoutName { get; set; } = "";

    public int CompletedExercises { get; set; }

    public int TotalExercises { get; set; }

    public DateTime? DateCompleted { get; set; }

    public ApplicationUser User { get; set; } = null!;

}
