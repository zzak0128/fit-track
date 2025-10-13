namespace FitTrack.Data.Models.Routines;

public class ExerciseImage
{
    public int Id { get; set; }

    public string RelativePath { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;
}
