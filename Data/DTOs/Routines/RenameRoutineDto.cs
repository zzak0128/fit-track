namespace FitTrack.Data.DTOs.Routines;

public class EditRoutineDto
{
    public int Id { get; set; }

    public string NewName { get; set; } = "";

    public string? Description { get; set; } = "";
}
