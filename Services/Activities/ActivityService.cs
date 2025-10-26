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
        Workout workout = await context.Workouts.Include(x => x.Routine).FirstOrDefaultAsync(x => x.Id == activityDto.WorkoutId) ?? throw new Exception("Unable to find the given workout.");
        var exerciseSets = await context.ExerciseSets.Include(x => x.Exercise).Where(x => x.Workout == workout).ToListAsync();
        List<WorkoutLog> workoutLogs = workout.ExerciseSets.Select(x => new WorkoutLog
        {
            Exercise = x.Exercise,
            ActivitySets = SetSetsCount(x.SetCount, x.Repetitions, x.Weight),
            SetCount = x.SetCount
        }).ToList();

        context.Attach(activityDto.User);

        Activity newActivity = new()
        {
            WorkoutLogs = workoutLogs,
            RoutineName = workout.Routine.Name,
            WorkoutName = workout.Name,
            DateCompleted = null,
            User = activityDto.User
        };

        context.Activities.Add(newActivity);
        await context.SaveChangesAsync();

        return newActivity.Id;
    }

    private List<ActivitySet> SetSetsCount(int setCount, int reps, double weight)
    {
        List<ActivitySet> activitySets = [];
        for (int i = 0; i < setCount; i++)
        {
            activitySets.Add(new ActivitySet
            {
                Repetitions = reps,
                Weight = weight
            });
        }

        if (!activitySets.Any())
        {
            activitySets.Add(new ActivitySet
            {
                Repetitions = reps,
                Weight = weight
            });
        }

        return activitySets;
    }

    public async Task<ActiveActivityDto> GetActiveActivityByIdAsync(int activityId, ApplicationUser CurrentUser)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        Activity activity = await context.Activities
                        .Include(x => x.WorkoutLogs)
                        .ThenInclude(x => x.ActivitySets)
                        .Include(x => x.WorkoutLogs)
                        .ThenInclude(y => y.Exercise)
                        .ThenInclude(z => z.Thumbnails)
                        .FirstOrDefaultAsync(x => x.Id == activityId && x.User == CurrentUser)
                        ?? throw new Exception("Unable to find the given activity");

        ActiveActivityDto activityDto = new()
        {
            ActivityId = activity.Id,
            RoutineName = activity.RoutineName,
            WorkoutName = activity.WorkoutName,
            DateCompleted = activity.DateCompleted,
            ExerciseList = activity.WorkoutLogs.Select(x => new WorkoutLogDto
            {
                WorkoutLogId = x.Id,
                IsCompleted = x.IsCompleted,
                Exercise = new ExerciseDto
                {
                    Id = x.Exercise.Id,
                    Name = x.Exercise.Name,
                    Description = x.Exercise.Description,
                    MuscleGroup = x.Exercise.MuscleGroup,
                    ImagePaths = x.Exercise.Thumbnails.Select(x => x.RelativePath).ToList()
                },
                ActivitySets = x.ActivitySets.Select(x => new ActivitySetDto
                {
                    Repetitions = x.Repetitions,
                    Weight = x.Weight
                }).ToList(),
                SetCount = x.SetCount
            }).ToList(),
            User = CurrentUser
        };

        return activityDto;
    }

    public async Task<List<ActivityListDto>> GetActivityListAsync(ApplicationUser currentUser)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        List<ActivityListDto> activityList = await context.Activities.Include(x => x.WorkoutLogs).ThenInclude(x => x.ActivitySets).Where(x => x.User == currentUser).Select(x => new ActivityListDto
        {
            ActivityId = x.Id,
            RoutineName = x.RoutineName,
            WorkoutName = x.WorkoutName,
            CompletedExercises = x.WorkoutLogs.Count(x => x.IsCompleted),
            TotalExercises = x.WorkoutLogs.Count(),
            DateCompleted = x.DateCompleted,
            User = x.User
        }).ToListAsync();

        return activityList;
    }

    public async Task<RecentActivityDto?> GetMostRecentCompletedActivityAsync(ApplicationUser currentUser)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var recentActivity = await context.Activities
            .Include(x => x.WorkoutLogs)
            .ThenInclude(x => x.ActivitySets)
            .Include(x => x.WorkoutLogs)
            .ThenInclude(x => x.Exercise)
            .OrderByDescending(x => x.DateCompleted)
            .FirstOrDefaultAsync(x => x.User == currentUser && x.DateCompleted.HasValue);

        if (recentActivity is null)
        {
            return null;
        }

        RecentActivityDto output = new()
        {
            RoutineName = recentActivity.RoutineName ?? "",
            WorkoutName = recentActivity.WorkoutName ?? "",
            DateCompleted = recentActivity.DateCompleted
        };

        foreach (var log in recentActivity.WorkoutLogs)
        {
            output.ExerciseList.Add(log.Exercise.Name);
            foreach (var set in log.ActivitySets)
            {
                output.TotalReps += set.Repetitions;
                output.TotalWeight += set.Weight * set.Repetitions;
            }
        }

        return output;
    }

    public async Task<RecordActivityDto> GetRecordActivityAsync(ApplicationUser currentUser)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        var activities = await context.Activities
            .Include(x => x.WorkoutLogs)
            .ThenInclude(x => x.ActivitySets)
            .OrderByDescending(x => x.DateCompleted)
            .Where(x => x.User == currentUser && x.DateCompleted.HasValue)
            .ToListAsync();

        List<(string routine, string exercise, int reps, double weight)> activityTotals = [];

        foreach (var activity in activities)
        {
            string routine = activity.RoutineName;
            string name = activity.WorkoutName;
            int repTotal = 0;
            double weightTotal = 0.00;

            foreach (var logs in activity.WorkoutLogs)
            {
                foreach (var set in logs.ActivitySets)
                {
                    repTotal += set.Repetitions;
                    weightTotal += set.Weight * set.Repetitions;
                }
            }

            activityTotals.Add((routine, name, repTotal, weightTotal));
        }


        BestRepsActivityDto bestRep = new();
        BestWeightActivityDto bestWeight = new();

        // Add logic here to find best set totals
        foreach (var activity in activityTotals)
        {
            if (activity.reps > bestRep.Reps)
            {
                bestRep.Routine = activity.routine;
                bestRep.Reps = activity.reps;
                bestRep.Exercise = activity.exercise;
            }

            if (activity.weight > bestWeight.Weight)
            {
                bestWeight.Routine = activity.routine;
                bestWeight.Weight = activity.weight;
                bestWeight.Exercise = activity.exercise;
            }
        }

        return new RecordActivityDto
        {
            BestReps = bestRep,
            BestWeight = bestWeight,
        };
    }

    public async Task SaveActivityAsync(ActiveActivityDto completeActivity, bool isCompleted = false)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        Activity activity = await context.Activities
            .Include(x => x.WorkoutLogs)
            .ThenInclude(x => x.ActivitySets)
            .Include(x => x.WorkoutLogs)
            .ThenInclude(x => x.Exercise)
            .FirstOrDefaultAsync(x => x.Id == completeActivity.ActivityId)
            ?? throw new Exception("Unable to find active activity.");

        activity.WorkoutLogs = completeActivity.ExerciseList
            .Select(x => new WorkoutLog
            {
                Id = x.WorkoutLogId,
                ActivitySets = x.ActivitySets.Select(x => new ActivitySet
                {
                    Weight = x.Weight,
                    Repetitions = x.Repetitions
                }).ToList(),
                SetCount = x.SetCount,
                Exercise = activity.WorkoutLogs.Select(y => y.Exercise).FirstOrDefault(z => z.Id == x.Exercise.Id)
                ?? context.Exercises.Find(x.Exercise.Id)
                ?? throw new Exception("Unable to find an exercise to save to this workout."),
                IsCompleted = x.IsCompleted
            }).ToList();

        if (isCompleted)
        {
            activity.DateCompleted = DateTime.Now;
        }

        context.Activities.Update(activity);
        await context.SaveChangesAsync();
    }

    public async Task CompleteWorkoutAsync(int logId)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        WorkoutLog log = await context.WorkoutLogs.FindAsync(logId) ?? throw new Exception("Unable to find this workout log");
        log.IsCompleted = true;
        context.WorkoutLogs.Update(log);

        await context.SaveChangesAsync();
    }

    public async Task CompleteActivityByIdAsync(int activityId)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        Activity activity = await context.Activities.FindAsync(activityId) ?? throw new Exception("Unable to find this activity");

        activity.DateCompleted = DateTime.Now;

        context.Update(activity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteActivityAsync(int activityId)
    {
        var context = await _contextFactory.CreateDbContextAsync();
        Activity activity = await context.Activities.FindAsync(activityId) ?? throw new Exception("Unable to find Activity with the given Id.");

        context.Activities.Remove(activity);
        await context.SaveChangesAsync();
    }
}
