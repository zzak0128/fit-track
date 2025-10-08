namespace FitTrack.Data.DTOs.ExerciseSets;

public class UpdateWorkoutExerciseSetDto
{
    public int WorkoutId { get; set; }

    public string WorkoutName { get; set; } = "";

    public List<ExerciseSetDto> ExerciseSets { get; set; } = [];
}
