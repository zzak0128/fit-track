using FitTrack.Data.DTOs.Exercises;

namespace FitTrack.Data.DTOs.ExerciseSets;

public class CreateExerciseSetDto
{
    public ExerciseDto Exercise { get; set; }

    public int Sequence { get; set; }

    public double Weight { get; set; }

    public int Repetitions { get; set; }

    public int SetCount { get; set; }
}
