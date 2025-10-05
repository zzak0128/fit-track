using FitTrack.Shared.Auth;

namespace FitTrack.Models.Measurements;

public class Measurement
{
    public int Id { get; set; }

    public string Metric { get; set; } = "";

    public double Number { get; set; }

    public DateTime Date { get; set; } = DateTime.Now;

    public ApplicationUser User { get; set; }
}
