using FitTrack.Data;
using FitTrack.Data.DTOs.Exercises;
using FitTrack.Data.DTOs.ExerciseSets;
using FitTrack.Data.DTOs.Routines;
using FitTrack.Data.DTOs.Workouts;
using FitTrack.Data.Models.Routines;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services.Routines;

public class RoutineService : IRoutineService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public RoutineService(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    // Exercises
    public async Task<List<ExerciseDto>> GetAllExercisesAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Exercises.Select(x => new ExerciseDto
        {
            Id = x.Id,
            Name = x.Name,
            MuscleGroup = x.MuscleGroup,
            Description = x.Description
        })
            .ToListAsync();
    }

    public async Task CreateExerciseAsync(CreateExerciseDto newExercise)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        context.Exercises.Add(new Exercise
        {
            Name = newExercise.Name,
            Description = newExercise.Description,
            MuscleGroup = newExercise.MuscleGroup
        });

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to create exercise", ex);
        }
    }

    public async Task DeleteExerciseAsync(int exerciseId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var exercise = await context.Exercises.FindAsync(exerciseId) ?? throw new Exception("Exercise not found");

        context.Exercises.Remove(exercise);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to delete exercise", ex);
        }
    }

    public async Task UpdateExerciseAsync(ExerciseDto exercise)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var exerciseToUpdate = await context.Exercises.FindAsync(exercise.Id) ?? throw new Exception("Exercise not found");

        exerciseToUpdate.Name = exercise.Name;
        exerciseToUpdate.Description = exercise.Description;
        exerciseToUpdate.MuscleGroup = exercise.MuscleGroup;

        context.Exercises.Update(exerciseToUpdate);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to update exercise", ex);
        }
    }

    // Routines

    public async Task AddWorkoutToRoutineAsync(int routineId, BaseWorkoutDto newWorkout)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var routine = await context.Routines.FindAsync(routineId) ?? throw new Exception("Unable to find the Routine");

        routine.Workouts.Add(new Workout
        {
            Name = newWorkout.Name
        });

        await context.SaveChangesAsync();
    }

    public async Task<List<BaseRoutineDto>> GetBaseRoutinesAsync(ApplicationUser user)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Routines
            .Select(x => new BaseRoutineDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                User = x.User
            }).Where(x => x.User == user)
            .ToListAsync();
    }

    public async Task<DetailRoutineDto> GetDetailRoutineByIdAsync(int routineId, ApplicationUser user)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        BaseRoutineDto? routine = await context.Routines
            .Where(x => x.Id == routineId && x.User.Id == user.Id)
            .Select(x => new BaseRoutineDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                User = x.User
            })
            .FirstOrDefaultAsync();

        List<BaseWorkoutDto> workouts = await context.Workouts
            .Where(x => x.Routine.Id == routineId)
            .Select(x => new BaseWorkoutDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();

        List<DetailExerciseSetDto> exerciseSets = [];
        foreach (var workout in workouts)
        {
            exerciseSets.AddRange(await context.ExerciseSets
                .Include(x => x.Exercise)
                .Where(x => x.Workout.Id == workout.Id)
                .Select(x => new DetailExerciseSetDto
                {
                    Id = x.Id,
                    Sequence = x.Sequence,
                    Exercise = new ExerciseDto
                    {
                        Id = x.Exercise.Id,
                        Name = x.Exercise.Name,
                        Description = x.Exercise.Description,
                        MuscleGroup = x.Exercise.MuscleGroup
                    },
                    Weight = x.Weight,
                    Repetitions = x.Repetitions,
                    SetCount = x.SetCount,
                    WorkoutId = x.Workout.Id
                })
                .ToListAsync());
        }

        if (routine == null)
        {
            throw new Exception("Routine not found or access denied");
        }

        return new DetailRoutineDto
        {
            Routine = routine,
            Workouts = workouts,
            ExerciseSets = exerciseSets,
            User = user
        };

    }

    public async Task CreateRoutineAsync(CreateRoutineDto routine)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        context.Routines.Add(new Routine
        {
            Name = routine.Name,
            Description = routine.Description,
            User = routine.User
        });

        context.Attach(routine.User);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to save the new routine", ex);
        }
    }

    public async Task DeleteRoutineAsync(int routineId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var routine = await context.Routines.FindAsync(routineId) ?? throw new Exception("Routine not found");

        context.Routines.Remove(routine);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to delete routine", ex);
        }
    }

    // Workouts
    public async Task<List<BaseWorkoutDto>> GetRoutineWorkoutsAsync(int routineId)
    {
        List<BaseWorkoutDto> workouts = [];
        await using var context = await _contextFactory.CreateDbContextAsync();
        workouts = await context.Workouts
            .Where(x => x.Routine.Id == routineId)
            .Select(x => new BaseWorkoutDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();

        return workouts;
    }

    public async Task RemoveWorkoutAsync(int workoutId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var workout = await context.Workouts.FindAsync(workoutId) ?? throw new Exception("Workout not found");

        context.Workouts.Remove(workout);
        await context.SaveChangesAsync();
    }

    // EXERCISE SETS

    // TODO: Implement this to add a new set to a workout
    public async Task AddExerciseSetToWorkoutAsync(int workoutId, CreateExerciseSetDto newSet)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var workout = await context.Workouts.FindAsync(workoutId) ?? throw new Exception("Unable to find the Routine");
        var exercise = await context.Exercises.FindAsync(newSet.Exercise.Id) ?? throw new Exception("Unable to find the Exercise");

        workout.ExerciseSets.Add(new ExerciseSet
        {
            Exercise = exercise
        });

        await context.SaveChangesAsync();
    }

    public async Task UpdateExerciseSetAsync(UpdateExerciseSetDto updateExerciseSet)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var exerciseSet = await context.ExerciseSets
            .FirstOrDefaultAsync(x => x.Id == updateExerciseSet.Id)
            ?? throw new Exception("Exercise Set not found");

        exerciseSet.Sequence = updateExerciseSet.Sequence;
        exerciseSet.Weight = updateExerciseSet.Weight;
        exerciseSet.Repetitions = updateExerciseSet.Repetitions;
        exerciseSet.SetCount = updateExerciseSet.SetCount;
        context.ExerciseSets.Update(exerciseSet);

        await context.SaveChangesAsync();
    }

    public async Task RemoveExerciseSetAsync(RemoveExerciseSetDto removeExerciseSet)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var workout = await context.Workouts
            .FirstOrDefaultAsync(x => x.Id == removeExerciseSet.WorkoutId)
            ?? throw new Exception("Workout not found");

        var exerciseSet = await context.ExerciseSets
            .FirstOrDefaultAsync(x => x.Id == removeExerciseSet.ExerciseSetId)
            ?? throw new Exception("Exercise Set not found");

        workout.ExerciseSets.Remove(exerciseSet);

        await context.SaveChangesAsync();
    }

    public async Task<List<ExerciseSetDto>> GetAllExerciseSetsAsync(int WorkoutId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var sets = await context.ExerciseSets
            .Include(x => x.Exercise)
            .Where(x => x.Workout.Id == WorkoutId)
            .Select(x => new ExerciseSetDto
            {
                Id = x.Id,
                Sequence = x.Sequence,
                Exercise = new ExerciseDto
                {
                    Id = x.Exercise.Id,
                    Name = x.Exercise.Name,
                    Description = x.Exercise.Description,
                    MuscleGroup = x.Exercise.MuscleGroup
                },
                Weight = x.Weight,
                Repetitions = x.Repetitions,
                SetCount = x.SetCount
            })
            .OrderBy(x => x.Sequence)
            .ToListAsync();

        return sets;
    }
}
