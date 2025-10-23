namespace FitTrack.Data.DTOs.Activities;

public class RecentActivityDto
{
    public string RoutineName { get; set; } = "";

    public string WorkoutName { get; set; } = "";

    public List<string> ExerciseList { get; set; } = [];

    public DateTime? DateCompleted { get; set; }

    public int TotalReps { get; set; }

    public double TotalWeight { get; set; }
}
