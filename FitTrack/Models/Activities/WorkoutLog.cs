using FitTrack.Shared.Auth;
using FitTrack.Models.Routines;

namespace FitTrack.Models.Activities;

public class WorkoutLog
{
    public int Id { get; set; }

    public Workout Workout { get; set; }

    public int SetCount { get; set; }

    public double Weight { get; set; }

    public int Repetitions { get; set; }

    public DateTime DateCompleted { get; set; }

    public int TimeElapsedSeconds { get; set; }

    public ApplicationUser User { get; set; }
}
