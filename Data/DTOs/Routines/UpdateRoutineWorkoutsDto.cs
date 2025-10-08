using FitTrack.Data.DTOs.Workouts;

namespace FitTrack.Data.DTOs.Routines;

public class UpdateRoutineWorkoutsDto
{
    public int Id { get; set; }

    public BaseRoutineDto BaseRoutine { get; set; }

    public List<BaseWorkoutDto> Workouts { get; set; } = [];
}
