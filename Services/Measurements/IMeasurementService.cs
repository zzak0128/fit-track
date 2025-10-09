using FitTrack.Data;
using FitTrack.Data.DTOs.Measurements;

namespace FitTrack.Services.Measurements;

public interface IMeasurementService
{
    Task AddMeasurementAsync(AddMeasurementDto measurement);
    Task<AddMeasurementDto> GetMeasurementByNameAsync(string measurementName, ApplicationUser user);
    Task<List<MeasurementChartDto>> GetMeasurementChartDataAsync(ApplicationUser currentUser);
    Task<List<string>> GetMeasurementNamesAsync(ApplicationUser currentUser);
    Task RemoveMeasurementTypeAsync(int measurementId);
}
