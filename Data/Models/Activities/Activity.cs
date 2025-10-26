namespace FitTrack.Data.Models.Activities;

public class Activity
{
    public int Id { get; set; }

    public string RoutineName { get; set; } = "";

    public string WorkoutName { get; set; } = "";

    public List<WorkoutLog> WorkoutLogs { get; set; } = [];

    public DateTime? DateCompleted { get; set; }

    public required ApplicationUser User { get; set; }
}
