namespace FitTrack.Data.DTOs.Routines;
public class CreateRoutineDto
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public ApplicationUser User { get; set; }
}
