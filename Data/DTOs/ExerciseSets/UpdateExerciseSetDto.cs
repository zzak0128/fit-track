using FitTrack.Data.DTOs.Exercises;

namespace FitTrack.Data.DTOs.ExerciseSets;

public class UpdateExerciseSetDto
{
    public int Id { get; set; }

    public int Sequence { get; set; }

    public double Weight { get; set; }

    public int Repetitions { get; set; }

    public int SetCount { get; set; }
}
