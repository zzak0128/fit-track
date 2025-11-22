using FitTrack.Data;
using FitTrack.Data.DTOs.FoodJournal;
using FitTrack.Data.Models.FoodJournal;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    // Meals
    public async Task<List<MealDto>> GetMealsByDateAsync(DateTime date, ApplicationUser currentUser)
    {
        var today = DateTime.Today.Date;

        var context = await _dbContextFactory.CreateDbContextAsync();
        var todaysMealCount = await context.Meals
            .Where(m => m.User == currentUser && m.Date.Date == date.Date)
            .CountAsync();

        if (todaysMealCount == 0)
        {
            List<Meal> newMeals = [];
            foreach (MealType mealType in Enum.GetValues(typeof(MealType)))
            {
                newMeals.Add(new Meal
                {
                    MealType = mealType,
                    Date = date,
                    User = currentUser
                });
            }

            context.Attach(currentUser);
            context.Meals.AddRange(newMeals);
            await context.SaveChangesAsync();

            return await GetMealsByDateAsync(date, currentUser);
        }
        else
        {
            return await context.Meals
                    .Include(m => m.Foods)
                    .ThenInclude(x => x.FoodItem)
                    .Select(m => new MealDto
                    {
                        Id = m.Id,
                        MealType = m.MealType,
                        Date = m.Date,
                        User = m.User,
                        FoodServings = m.Foods.Select(x => new MealFoodServingDto
                        {
                            Id = x.Id,
                            FoodItem = new FoodItemDto
                            {
                                Id = x.FoodItem.Id,
                                Name = x.FoodItem.Name,
                                ServingSize = x.FoodItem.ServingSize,
                                Units = x.FoodItem.Units,
                                Calories = x.FoodItem.Calories,
                                Carbs = x.FoodItem.Carbs,
                                Fats = x.FoodItem.Fats,
                                Proteins = x.FoodItem.Proteins
                            },
                            Servings = x.Servings
                        }).OrderBy(x => x.FoodItem.Name).ToList()
                    })
                    .Where(m => m.User == currentUser && m.Date.Date == date.Date)
                    .ToListAsync();
        }
    }

    public async Task CopyToDateAsync(MealDto copyFromMeal, DateTime copyToDate, MealType mealType)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var daysMealCount = await context.Meals
            .Where(m => m.User == copyFromMeal.User && m.Date.Date == copyToDate.Date)
            .CountAsync();

        if (daysMealCount == 0)
        {
            List<Meal> newMeals = [];
            foreach (MealType mType in Enum.GetValues<MealType>())
            {
                newMeals.Add(new Meal
                {
                    MealType = mType,
                    Date = copyToDate,
                    User = copyFromMeal.User
                });
            }

            context.Attach(copyFromMeal.User);
            context.Meals.AddRange(newMeals);
        }

        List<(FoodItem, double)> copyFromFood = [];
        foreach (var serving in copyFromMeal.FoodServings)
        {
            copyFromFood.Add(
                (await context.FoodItems.FindAsync(serving.FoodItem.Id) ?? throw new Exception("Could not find food item."),
                serving.Servings));
        }

        var copyTo = await context.Meals
            .Include(x => x.Foods)
            .ThenInclude(x => x.FoodItem)
            .Where(m => m.User == copyFromMeal.User && m.Date.Date == copyToDate.Date && m.MealType == mealType)
            .FirstOrDefaultAsync();

        foreach (var food in copyFromFood)
        {
            copyTo?.Foods.Add(new MealFoodServing
            {
                FoodItem = food.Item1,
                Servings = food.Item2
            });
        }
        context.Meals.Update(copyTo!);
        await context.SaveChangesAsync();
    }

    public async Task AddFoodToMealAsync(int mealId, MealFoodServingDto foodServing)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var mealToUpdate = await context.Meals.FindAsync(mealId) ?? throw new Exception("Unable to find this meal to update.");
        var foodItemToAdd = await context.FoodItems.FindAsync(foodServing.FoodItem.Id) ?? throw new Exception("Unable to find this food item");

        MealFoodServing servingToAdd = new()
        {
            FoodItem = foodItemToAdd,
            Servings = foodServing.Servings
        };

        mealToUpdate.Foods.Add(servingToAdd);
        await context.SaveChangesAsync();
    }

    public async Task DeleteMealFoodServingAsync(int id)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var servingToDelete = await context.MealFoodServings.FindAsync(id) ?? throw new Exception("Unable to find this meal serving to remove.");

        context.MealFoodServings.Remove(servingToDelete);
        await context.SaveChangesAsync();
    }


    // User Goals
    public async Task<UserGoalsDto> GetUserGoalsAsync(ApplicationUser currentUser)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.UserGoals
            .Where(ug => ug.User == currentUser)
            .Select(ug => new UserGoalsDto
            {
                Id = ug.Id,
                Calories = ug.Calories,
                Carbs = ug.Carbs,
                Fats = ug.Fats,
                Protein = ug.Protein,
                User = ug.User
            })
            .FirstOrDefaultAsync() ?? new UserGoalsDto();
    }

    public async Task SaveUserGoalsAsync(UserGoalsDto userGoals, ApplicationUser currentUser)
    {
        var context = await _dbContextFactory.CreateDbContextAsync();
        var goalToUpdate = await context.UserGoals.FindAsync(userGoals.Id) ?? new UserGoals
        {
            User = currentUser
        };

        goalToUpdate.Calories = userGoals.Calories;
        goalToUpdate.Carbs = userGoals.Carbs;
        goalToUpdate.Fats = userGoals.Fats;
        goalToUpdate.Protein = userGoals.Protein;

        if (goalToUpdate.Id == 0)
        {
            context.UserGoals.Add(goalToUpdate);
        }
        else
        {
            context.UserGoals.Update(goalToUpdate);
        }

        context.Attach(currentUser);

        await context.SaveChangesAsync();
    }
}
