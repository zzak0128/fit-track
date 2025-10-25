namespace FitTrack.Data.Models.Measurements;

public class MeasurementData
{
    public int Id { get; set; }

    public double Amount { get; set; }

    public required string Unit { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public virtual Measurement Measurement { get; set; } = null!;
}
