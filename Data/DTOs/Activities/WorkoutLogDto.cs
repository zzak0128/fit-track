using FitTrack.Data.DTOs.Exercises;

namespace FitTrack.Data.DTOs.Activities;

public class WorkoutLogDto
{
    public int WorkoutLogId { get; set; }

    public ExerciseDto Exercise { get; set; }

    public List<ActivitySetDto> ActivitySets { get; set; } = [];

    public int SetCount { get; set; }

    public bool IsCompleted { get; set; }
}
