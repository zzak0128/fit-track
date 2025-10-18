using FitTrack.Data;
using FitTrack.Data.DTOs.Activities;
using FitTrack.Data.DTOs.Exercises;
using FitTrack.Data.Models.Activities;
using FitTrack.Data.Models.Routines;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.Activities;

public class ActivityService : IActivityService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public ActivityService(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<int> CreateActivityAsync(CreateActivityDto activityDto)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        Workout workout = await context.Workouts.FindAsync(activityDto.WorkoutId) ?? throw new Exception("Unable to find the given workout.");
        var exerciseSets = await context.ExerciseSets.Include(x => x.Exercise).Where(x => x.Workout == workout).ToListAsync();
        List<WorkoutLog> workoutLogs = workout.ExerciseSets.Select(x => new WorkoutLog
        {
            Exercise = x.Exercise,
            SetCount = x.SetCount,
            Weight = x.Weight,
            Repetitions = x.Repetitions,
            DateCompleted = null
        }).ToList();

        context.Attach(activityDto.User);

        Activity newActivity = new()
        {
            WorkoutLogs = workoutLogs,
            WorkoutName = workout.Name,
            User = activityDto.User
        };

        context.Activities.Add(newActivity);
        await context.SaveChangesAsync();

        return newActivity.Id;
    }


    public async Task<ActiveActivityDto> GetActiveActivityByIdAsync(int activityId, ApplicationUser CurrentUser)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        Activity activity = await context.Activities
            .Include(x => x.WorkoutLogs)
            .ThenInclude(y => y.Exercise)
            .ThenInclude(z => z.Thumbnails)
            .FirstOrDefaultAsync(x => x.Id == activityId && x.User == CurrentUser)
            ?? throw new Exception("Unable to find the given activity");

        ActiveActivityDto activityDto = new()
        {
            ActivityId = activity.Id,
            WorkoutName = activity.WorkoutName,
            ExerciseList = activity.WorkoutLogs.Select(x => new WorkoutLogDto
            {
                WorkoutLogId = x.Id,
                Exercise = new ExerciseDto
                {
                    Id = x.Exercise.Id,
                    Name = x.Exercise.Name,
                    Description = x.Exercise.Description,
                    MuscleGroup = x.Exercise.MuscleGroup,
                    ImagePaths = x.Exercise.Thumbnails.Select(x => x.RelativePath).ToList()
                },
                SetCount = x.SetCount,
                Weight = x.Weight,
                Repetitions = x.Repetitions,
                DateCompleted = x.DateCompleted
            }).ToList(),
            User = CurrentUser
        };

        return activityDto;
    }

    public async Task<List<ActivityListDto>> GetActivityListAsync()
    {
        var context = await _contextFactory.CreateDbContextAsync();
        List<ActivityListDto> activityList = await context.Activities.Select(x => new ActivityListDto
        {
            ActivityId = x.Id,
            WorkoutName= x.WorkoutName,
            User = x.User
        }).ToListAsync();

        return activityList;
    }

    public async Task DeleteActivityAsync(int activityId)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        Activity activity = await context.Activities.FindAsync(activityId) ?? throw new Exception("Unable to find Activity with the given Id.");

        context.Activities.Remove(activity);
        await context.SaveChangesAsync();
    }
}
