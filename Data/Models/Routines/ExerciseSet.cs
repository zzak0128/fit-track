namespace FitTrack.Data.Models.Routines;

public class ExerciseSet
{
    public int Id { get; set; }

    public Exercise Exercise { get; set; }

    public double Weight { get; set; }

    public int Repetitions { get; set; }

    public int SetCount { get; set; }
}
