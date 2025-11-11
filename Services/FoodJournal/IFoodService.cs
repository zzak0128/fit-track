using FitTrack.Data.DTOs.FoodJournal;

namespace FitTrack.Services.FoodJournal;

public interface IFoodService
{
    Task CreateFoodItemAsync(CreateFoodItemDto newFoodDto);
    Task<List<FoodItemDto>> GetAllFoodAsync();
}
