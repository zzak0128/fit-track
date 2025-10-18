using FitTrack.Data.DTOs.Exercises;

namespace FitTrack.Data.DTOs.Activities;

public class WorkoutLogDto
{
    public int WorkoutLogId { get; set; }

    public ExerciseDto Exercise { get; set; }

    public int SetCount { get; set; }

    public double Weight { get; set; }

    public int Repetitions { get; set; }

    public DateTime? DateCompleted { get; set; }
}
