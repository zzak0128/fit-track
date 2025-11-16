using FitTrack.Data;
using FitTrack.Data.DTOs.FoodJournal;

namespace FitTrack.Services.FoodJournal;

public interface IFoodService
{
    Task CreateFoodItemAsync(CreateFoodItemDto newFoodDto);
    Task DeleteFoodItemAsync(int deleteFoodId);
    Task<List<FoodItemDto>> GetAllFoodAsync();
    Task<List<MealDto>> GetTodaysMealsAsync(ApplicationUser currentUser);
    Task UpdateFoodItemAsync(FoodItemDto foodItem);
}
