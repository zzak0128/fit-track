namespace FitTrack.Data.Models.Routines;

public class ExerciseImage
{
    public int Id { get; set; }

    public required string RelativePath { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;
}
