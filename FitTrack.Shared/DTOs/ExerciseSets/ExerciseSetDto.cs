using FitTrack.Shared.DTOs.Exercises;

namespace FitTrack.Shared.DTOs.ExerciseSets;
public class ExerciseSetDto
{
    public int Id { get; set; }

    public ExerciseDto Exercise { get; set; }

    public double Weight { get; set; }

    public int Repetitions { get; set; }

    public int SetCount { get; set; }
}
