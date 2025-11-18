using FitTrack.Data;
using FitTrack.Data.DTOs.FoodJournal;

namespace FitTrack.Services.FoodJournal;

public interface IFoodService
{
    Task AddFoodToMealAsync(int mealId, MealFoodServingDto foodServing);
    Task CreateFoodItemAsync(CreateFoodItemDto newFoodDto);
    Task DeleteFoodItemAsync(int deleteFoodId);
    Task DeleteMealFoodServingAsync(int id);
    Task<List<FoodItemDto>> GetAllFoodAsync();
    Task<List<MealDto>> GetMealsByDateAsync(DateTime date, ApplicationUser currentUser);
    Task UpdateFoodItemAsync(FoodItemDto foodItem);
}
