using FitTrack.Data;
using FitTrack.Data.DTOs.Measurements;

namespace FitTrack.Services.Measurements;

public interface IMeasurementService
{
    Task AddMeasurementAsync(AddMeasurementDto measurement);
    Task<MeasurementDto> GetMeasurementByIdAsync(int measurementId, ApplicationUser user);
    Task<AddMeasurementDto> GetMeasurementByNameAsync(string measurementName, ApplicationUser user);
    Task<List<MeasurementChartDto>> GetMeasurementChartDataAsync(ApplicationUser currentUser);
    Task<MeasurementChartDto?> GetMeasurementChartDataAsync(string measurementName, ApplicationUser currentUser);
    Task<List<string>> GetMeasurementNamesAsync(ApplicationUser currentUser);
    Task RemoveMeasurementDataAsync(int measurementDataId);
    Task RemoveMeasurementTypeAsync(int measurementId);
}
