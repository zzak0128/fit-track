namespace FitTrack.Data.DTOs.Exercises;

public class ExerciseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public MuscleGroup MuscleGroup { get; set; }

    public string? Description { get; set; }

    public List<string> ImagePaths { get; set; } = [];
}
