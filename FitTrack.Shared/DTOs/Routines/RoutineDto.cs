using FitTrack.Shared.Auth;
using FitTrack.Shared.DTOs.Workouts;

namespace FitTrack.Shared.DTOs.Routines;
public class RoutineDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "My Routine";

    public string? Description { get; set; }

    public List<WorkoutDto> Workouts { get; set; } = [];

    public required ApplicationUser User { get; set; }
}
