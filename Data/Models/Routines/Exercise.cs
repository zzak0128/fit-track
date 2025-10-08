namespace FitTrack.Data.Models.Routines;

public class Exercise
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public MuscleGroup MuscleGroup { get; set; }

    public string? Description { get; set; }
}
