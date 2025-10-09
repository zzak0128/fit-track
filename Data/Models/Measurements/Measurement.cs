
namespace FitTrack.Data.Models.Measurements;

public class Measurement
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public ApplicationUser User { get; set; }

    public virtual List<MeasurementData> MeasurementData { get; set; } = [];
}
