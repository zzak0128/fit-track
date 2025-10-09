namespace FitTrack.Data.DTOs.Measurements;

public class AddMeasurementDto
{
    public string Name { get; set; }

    public double Amount { get; set; }

    public string Unit { get; set; } = "";

    public DateTime Date { get; set; } = DateTime.Now;

    public ApplicationUser User { get; set; }
}
