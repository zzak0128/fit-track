namespace FitTrack.Data.DTOs.Measurements;

public class MeasurementDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public List<MeasurementDataDto> MeasurementData { get; set; } = [];

    public ApplicationUser User { get; set; } = null!;
}
