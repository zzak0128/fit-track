using FitTrack.Shared.Auth;
using FitTrack.Models.Routines;

namespace FitTrack.Models.Activities;

public class Activity
{
    public int Id { get; set; }

    public Workout Workout { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public ApplicationUser User { get; set; }
}
