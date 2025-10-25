using FitTrack.Components.Pages.Measurements;
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

    public async Task<MeasurementDto> GetMeasurementByIdAsync(int measurementId, ApplicationUser user)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var measurement = await context.Measurements
            .Include(m => m.MeasurementData)
            .Include(m => m.User)
            .Where(x => x.Id == measurementId && x.User == user)
            .FirstOrDefaultAsync(m => m.Id == measurementId) ?? throw new Exception("Measurement not found.");

        return new MeasurementDto
        {
            Id = measurement.Id,
            Name = measurement.Name,
            User = measurement.User,
            MeasurementData = [.. measurement.MeasurementData.Select(md => new MeasurementDataDto
            {
                Id = md.Id,
                Amount = md.Amount,
                Unit = md.Unit,
                Date = md.Date
            })
            .OrderBy(x => x.Date)]
        };
    }

    public async Task<AddMeasurementDto> GetMeasurementByNameAsync(string measurementName, ApplicationUser user)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var measurement = await context.Measurements
            .Include(m => m.MeasurementData)
            .FirstOrDefaultAsync(m => m.Name == measurementName && m.User == user) ?? throw new Exception("Measurement not found.");

        return new AddMeasurementDto
        {
            Name = measurement.Name,
            Unit = measurement.MeasurementData.Select(x => x.Unit).FirstOrDefault()!,
        };
    }

    public async Task AddMeasurementAsync(AddMeasurementDto measurement)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var existingMeasurement = await context.Measurements
            .Include(m => m.MeasurementData)
            .Include(m => m.User)
            .FirstOrDefaultAsync(m => m.Name == measurement.Name && m.User.Id == measurement.User.Id);

        if (existingMeasurement != null)
        {
            existingMeasurement.MeasurementData.Add(new MeasurementData
            {
                Amount = measurement.Amount,
                Unit = measurement.Unit,
                Date = measurement.Date.GetValueOrDefault()
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
                        Date = measurement.Date.GetValueOrDefault()
                    }
                ]
            });

            context.Attach(measurement.User);
        }

        await context.SaveChangesAsync();
    }

    public async Task<MeasurementChartDto?> GetMeasurementChartDataAsync(string measurementName, ApplicationUser currentUser)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var measurement = await context.Measurements
            .Include(m => m.MeasurementData)
            .FirstOrDefaultAsync(m => m.Name == measurementName && m.User == currentUser) ?? throw new Exception("Measurement not found.");

        if (measurement.MeasurementData is null)
        {
            return null;
        }

        MeasurementChartDto output = new();
        var name = measurement.Name;

        List<ChartSeries> chartSeries = [];
        ChartSeries dataChart = new()
        {
            Name = measurement.MeasurementData.Select(x => x.Unit)
                .FirstOrDefault()!,
            Data = measurement.MeasurementData
                .OrderBy(x => x.Date)
                .Select(x => x.Amount)
                .ToArray(),
            ShowDataMarkers = true
        };

        chartSeries.Add(dataChart);
        var measurementDates = measurement.MeasurementData
            .OrderBy(x => x.Date)
            .Select(x => x.Date.ToString("M/d"))
            .ToArray();

        output = new MeasurementChartDto
        {
            MeasurementId = measurement.Id,
            Name = name,
            Series = chartSeries,
            Dates = measurementDates
        };

        return output;
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

            if (measurement.MeasurementData is null)
            {
                continue;
            }

            ChartSeries dataChart = new()
            {
                Name = measurement.MeasurementData
                    .Select(x => x.Unit)
                    .FirstOrDefault()!,
                Data = measurement.MeasurementData
                    .OrderBy(x => x.Date)
                    .Select(x => x.Amount)
                    .ToArray(),
                ShowDataMarkers = true
            };

            chartSeries.Add(dataChart);
            var measurementDates = measurements.SelectMany(x => x.MeasurementData)
                .OrderBy(x => x.Date)
                .Select(x => x.Date.ToString("M/d"))
                .ToArray();

            output.Add(new MeasurementChartDto
            {
                MeasurementId = measurement.Id,
                Name = name,
                Series = chartSeries,
                Dates = measurementDates
            });
        }

        return output;
    }

    public async Task RemoveMeasurementTypeAsync(int measurementId)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var measurement = await context.Measurements.FindAsync(measurementId) ?? throw new Exception("Unable to delete the Measurement.");

        context.Measurements.Remove(measurement);
        await context.SaveChangesAsync();
    }

    public async Task RemoveMeasurementDataAsync(int measurementDataId)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var measurementData = await context.MeasurementData.FindAsync(measurementDataId) ?? throw new Exception("Unable to delete the Measurement Data.");

        context.MeasurementData.Remove(measurementData);
        await context.SaveChangesAsync();
    }
}
