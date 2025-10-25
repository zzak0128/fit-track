using FitTrack.Data.Models.Routines;

namespace FitTrack.Data.Models.Activities;

public class WorkoutLog
{
    public int Id { get; set; }

    public Exercise Exercise { get; set; } = null!;

    public List<ActivitySet> ActivitySets { get; set; } = [];

    public int SetCount { get; set; }

    public bool IsCompleted { get; set; }

    public virtual Activity Activity { get; set; } = null!;
}
