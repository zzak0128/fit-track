using MudBlazor;

namespace FitTrack.Data.DTOs.Measurements;

public class MeasurementChartDto
{
    public int MeasurementId { get; set; }

    public string Name { get; set; } = "";

    public List<ChartSeries> Series { get; set; } = [];

    public string[] Dates { get; set; } = [];
}
