namespace FitTrack.Data.Models.Activities;

public class ActivitySet
{
    public int Id { get; set; }

    public int Repetitions { get; set; }

    public double Weight { get; set; }

    public virtual WorkoutLog WorkoutLog { get; set; } = null!;
}
