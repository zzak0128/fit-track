namespace FitTrack.Data.DTOs.Exercises;

public class ExerciseCsvRecord
{
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public MuscleGroup MuscleGroup { get; set; } = MuscleGroup.General;

    public string ImagePaths { get; set; }
}
