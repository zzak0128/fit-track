using FitTrack.Data.Models.FoodJournal;

namespace FitTrack.Data.DTOs.FoodJournal;

public class MealDto
{
    public int Id { get; set; }

    public List<MealFoodServingDto> FoodServings { get; set; } = [];

    public DateTime Date { get; set; }

    public MealType MealType { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
