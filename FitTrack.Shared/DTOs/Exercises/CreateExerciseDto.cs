namespace FitTrack.Shared.DTOs.Exercises;
public class CreateExerciseDto()
{
    public string Name { get; set; } = "";

    public string? Description { get; set; }

    public MuscleGroup MuscleGroup { get; set; }
}
