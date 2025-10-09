using FitTrack.Data.DTOs.Exercises;

namespace FitTrack.Data.DTOs.ExerciseSets;

public class CreateExerciseSetDto
{
    public int WorkoutId { get; set; }

    public ExerciseDto Exercise { get; set; }
}
