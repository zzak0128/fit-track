using FitTrack.Shared.Auth;
using FitTrack.Shared.DTOs.ExerciseSets;

namespace FitTrack.Shared.DTOs.Workouts;
public class WorkoutDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "My Workout";

    public List<ExerciseSetDto> ExerciseSets { get; set; } = [];

    public ApplicationUser User { get; set; }
}
