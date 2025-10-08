using FitTrack.Data.Models.Routines;

namespace FitTrack.Data.Models.Activities;

public class Activity
{
    public int Id { get; set; }

    public Workout Workout { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public ApplicationUser User { get; set; }
}
