using FitTrack.Data;
using FitTrack.Data.DTOs.FoodJournal;
using FitTrack.Data.Models.FoodJournal;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.FoodJournal;

public class FoodService : IFoodService
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public FoodService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    // Food Items
    public async Task<List<FoodItemDto>> GetAllFoodAsync()
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.FoodItems.Select(x => new FoodItemDto
        {
            Id = x.Id,
            Name = x.Name,
            ServingSize = x.ServingSize,
            Units = x.Units,
            Calories = x.Calories,
            Carbs = x.Carbs,
            Fats = x.Fats,
            Proteins = x.Proteins
        }).OrderBy(x => x.Name)
        .ToListAsync();
    }

    public async Task CreateFoodItemAsync(CreateFoodItemDto newFoodDto)
    {
        FoodItem newFood = new()
        {
            Name = newFoodDto.Name,
            ServingSize = newFoodDto.ServingSize,
            Units = newFoodDto.Units,
            Calories = newFoodDto.Calories,
            Carbs = newFoodDto.Carbs,
            Fats = newFoodDto.Fats,
            Proteins = newFoodDto.Proteins
        };

        var context = await _dbContextFactory.CreateDbContextAsync();
        context.FoodItems.Add(newFood);
        await context.SaveChangesAsync();
    }

    public async Task DeleteFoodItemAsync(int deleteFoodId)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var deleteFoodItem = await context.FoodItems.FindAsync(deleteFoodId) ?? throw new Exception("Unable to find the food to delete with this ID.");

        context.FoodItems.Remove(deleteFoodItem);
        await context.SaveChangesAsync();
    }

    public async Task UpdateFoodItemAsync(FoodItemDto foodItem)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var updateFoodItem = await context.FoodItems.FindAsync(foodItem.Id) ?? throw new Exception("Unable to find the food to update with this ID.");
        updateFoodItem.Name = foodItem.Name;
        updateFoodItem.ServingSize = foodItem.ServingSize;
        updateFoodItem.Units = foodItem.Units;
        updateFoodItem.Calories = foodItem.Calories;
        updateFoodItem.Carbs = foodItem.Carbs;
        updateFoodItem.Fats = foodItem.Fats;
        updateFoodItem.Proteins = foodItem.Proteins;

        context.FoodItems.Update(updateFoodItem);
        await context.SaveChangesAsync();
    }
}
