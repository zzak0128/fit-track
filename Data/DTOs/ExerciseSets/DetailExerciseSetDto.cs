using FitTrack.Data.DTOs.Exercises;

namespace FitTrack.Data.DTOs.ExerciseSets;

public class DetailExerciseSetDto
{
    public int Id { get; set; }

    public int Sequence { get; set; }

    public ExerciseDto Exercise { get; set; } = null!;

    public double Weight { get; set; }

    public int Repetitions { get; set; }

    public int SetCount { get; set; }


    public int WorkoutId { get; set; }
}
