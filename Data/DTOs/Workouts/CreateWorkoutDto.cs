using FitTrack.Data.DTOs.Routines;

namespace FitTrack.Data.DTOs.Workouts;

public class CreateWorkoutDto
{
    public string Name { get; set; } = "My Workout";

    public int RoutineId { get; set; }
}
