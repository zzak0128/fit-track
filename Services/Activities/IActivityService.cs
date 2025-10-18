using FitTrack.Data;
using FitTrack.Data.DTOs.Activities;

namespace FitTrack.Services.Activities;

public interface IActivityService
{
    Task<int> CreateActivityAsync(CreateActivityDto activityDto);
    Task DeleteActivityAsync(int activityId);
    Task<ActiveActivityDto> GetActiveActivityByIdAsync(int ActivityId, ApplicationUser CurrentUser);
    Task<List<ActivityListDto>> GetActivityListAsync();
}
