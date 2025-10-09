namespace FitTrack.Data.DTOs.Measurements;

public class MeasurementDataDto
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public DateTime Date { get; set; }

    public string Unit { get; set; } = "";
}
