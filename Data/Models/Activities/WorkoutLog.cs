using FitTrack.Data.Models.Routines;

namespace FitTrack.Data.Models.Activities;

public class WorkoutLog
{
    public int Id { get; set; }

    public Exercise Exercise { get; set; }

    public int SetCount { get; set; }

    public double Weight { get; set; }

    public int Repetitions { get; set; }

    public DateTime? DateCompleted { get; set; }

    public virtual Activity Activity { get; set; } = null!;
}
