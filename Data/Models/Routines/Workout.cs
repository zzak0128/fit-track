namespace FitTrack.Data.Models.Routines;

public class Workout
{
    public int Id { get; set; }

    public string Name { get; set; } = "My Workout";

    public List<ExerciseSet> ExerciseSets { get; set; } = [];

    public virtual Routine Routine { get; set; }
}
