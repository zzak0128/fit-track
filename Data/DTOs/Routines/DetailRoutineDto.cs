using FitTrack.Data.DTOs.ExerciseSets;
using FitTrack.Data.DTOs.Workouts;

namespace FitTrack.Data.DTOs.Routines;

public class DetailRoutineDto
{
    public BaseRoutineDto Routine { get; set; } = null!;

    public List<BaseWorkoutDto> Workouts { get; set; } = [];

    public List<DetailExerciseSetDto> ExerciseSets { get; set; } = [];

    public ApplicationUser User { get; set; } = null!;
}
