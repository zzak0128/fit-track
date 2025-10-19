using FitTrack.Data.DTOs.ExerciseSets;

namespace FitTrack.Data.DTOs.Activities;

public class ActiveActivityDto
{
    public int ActivityId { get; set; }

    public string WorkoutName { get; set; }

    public List<WorkoutLogDto> ExerciseList { get; set; }

    public DateTime? DateCompleted { get; set; }

    public ApplicationUser User { get; set; }
}
