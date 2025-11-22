using FitTrack.Data;
using FitTrack.Data.DTOs.FoodJournal;
using FitTrack.Data.Models.FoodJournal;

namespace FitTrack.Services.FoodJournal;

public interface IFoodService
{
    Task AddFoodToMealAsync(int mealId, MealFoodServingDto foodServing);
    Task CopyToDateAsync(MealDto copyFromMeal, DateTime copyToDate, MealType mealType);
    Task CreateFoodItemAsync(CreateFoodItemDto newFoodDto);
    Task DeleteFoodItemAsync(int deleteFoodId);
    Task DeleteMealFoodServingAsync(int id);
    Task<List<FoodItemDto>> GetAllFoodAsync();
    Task<List<MealDto>> GetMealsByDateAsync(DateTime date, ApplicationUser currentUser);
    Task<UserGoalsDto> GetUserGoalsAsync(ApplicationUser currentUser);
    Task SaveUserGoalsAsync(UserGoalsDto userGoals, ApplicationUser currentUser);
    Task UpdateFoodItemAsync(FoodItemDto foodItem);
}
