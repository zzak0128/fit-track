namespace FitTrack.Data.Models.Routines;

public class Routine
{
    public int Id { get; set; }

    public string Name { get; set; } = "My Routine";

    public string? Description { get; set; }

    public List<Workout> Workouts { get; set; } = [];

    public ApplicationUser User { get; set; }
}
