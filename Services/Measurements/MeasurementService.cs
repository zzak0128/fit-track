using FitTrack.Data;
using FitTrack.Data.DTOs.Measurements;
using FitTrack.Data.Models.Measurements;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace FitTrack.Services.Measurements;

public class MeasurementService : IMeasurementService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public MeasurementService(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<List<string>> GetMeasurementNamesAsync(ApplicationUser currentUser)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var measurements = await context.Measurements
            .Include(m => m.User)
            .Where(x => x.User == currentUser)
            .ToListAsync();

        return measurements.Select(x => x.Name).ToList();
    }

    public async Task AddMeasurementAsync(AddMeasurementDto measurement)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var existingMeasurement = await context.Measurements
            .Include(m => m.MeasurementData)
            .FirstOrDefaultAsync(m => m.Name == measurement.Name && m.User.Id == measurement.User.Id);

        if (existingMeasurement != null)
        {
            existingMeasurement.MeasurementData.Add(new MeasurementData
            {
                Amount = measurement.Amount,
                Unit = measurement.Unit,
                Date = measurement.Date
            });
        }
        else
        {
            context.Measurements.Add(new Measurement
            {
                Name = measurement.Name,
                User = measurement.User,
                MeasurementData =
                [
                    new MeasurementData
                    {
                        Amount = measurement.Amount,
                        Unit = measurement.Unit,
                        Date = measurement.Date
                    }
                ]
            });

            context.Attach(measurement.User);
        }

        await context.SaveChangesAsync();
    }

    public async Task<List<MeasurementChartDto>> GetMeasurementChartDataAsync(ApplicationUser currentUser)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var measurements = await context.Measurements
            .Include(m => m.MeasurementData)
            .Include(m => m.User)
            .Where(x => x.User == currentUser)
            .ToListAsync();

        List<MeasurementChartDto> output = [];
        foreach (var measurement in measurements)
        {
            var name = measurement.Name;

            List<ChartSeries> chartSeries = [];
            ChartSeries dataChart = new()
            {
                Name = measurement.MeasurementData.Select(x => x.Unit).FirstOrDefault(),
                Data = measurement.MeasurementData
                    .OrderBy(x => x.Date)
                    .Select(x => x.Amount)
                    .ToArray()
            };

            chartSeries.Add(dataChart);



            var measurementDates = measurements.SelectMany(x => x.MeasurementData)
                .OrderBy(x => x.Date)
                .Select(x => x.Date.ToShortDateString())
                .ToArray();



            output.Add(new MeasurementChartDto
            {
                Name = name,
                Series = chartSeries,
                Dates = measurementDates
            });
        }

        return output;
    }
}
