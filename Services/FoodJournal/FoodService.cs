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
}
