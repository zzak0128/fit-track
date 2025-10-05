using FitTrack.Shared.Auth;

namespace FitTrack.Shared.DTOs.Routines;
public class CreateRoutineDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required ApplicationUser User { get; set; }
}
