namespace FitTrack.Data.Models.Activities;

public class Activity
{
    public int Id { get; set; }

    public string WorkoutName { get; set; }

    public List<WorkoutLog> WorkoutLogs { get; set; }

    public ApplicationUser User { get; set; }
}
