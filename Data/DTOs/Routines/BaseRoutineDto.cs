namespace FitTrack.Data.DTOs.Routines;
public class BaseRoutineDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "My Routine";

    public string? Description { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
