using FitTrack.Data.DTOs.FoodJournal;

namespace FitTrack.Services.FoodJournal;

public interface IFoodService
{
    Task CreateFoodItemAsync(CreateFoodItemDto newFoodDto);
    Task DeleteFoodItemAsync(int deleteFoodId);
    Task<List<FoodItemDto>> GetAllFoodAsync();
    Task UpdateFoodItemAsync(FoodItemDto foodItem);
}
