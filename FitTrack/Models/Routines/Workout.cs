
using FitTrack.Shared.Auth;

namespace FitTrack.Models.Routines;

public class Workout
{
    public int Id { get; set; }

    public string Name { get; set; } = "My Workout";

    public List<ExerciseSet> ExerciseSets { get; set; } = [];

    public ApplicationUser User { get; set; }
}
