using FitTrack.Data;
using FitTrack.Data.DTOs.Exercises;
using FitTrack.Data.DTOs.ExerciseSets;
using FitTrack.Data.DTOs.Routines;
using FitTrack.Data.DTOs.Workouts;
using FitTrack.Data.Models.Routines;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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

    public async Task UpdateRoutineAsync(UpdateRoutineWorkoutsDto updateRoutine)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var routineToUpdate = await context.Routines
            .Include(x => x.Workouts)
            .FirstOrDefaultAsync(x => x.Id == updateRoutine.BaseRoutine.Id)
            ?? throw new Exception("Routine not found");

        routineToUpdate.Name = updateRoutine.BaseRoutine.Name;
        routineToUpdate.Description = updateRoutine.BaseRoutine.Description;

        List<Workout> updateWorkouts = updateRoutine.Workouts.Select(w => new Workout
        {
            Id = w.Id,
            Name = w.Name,
            Routine = routineToUpdate
        }).ToList();

        routineToUpdate.Workouts = updateWorkouts;
        context.Entry(routineToUpdate).CurrentValues.SetValues(updateWorkouts);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to update routine", ex);
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

    public async Task CreateWorkoutAsync(CreateWorkoutDto workout)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var UpdateRoutine = await context.Routines.FindAsync(workout.RoutineId) ?? throw new Exception("Routine not found");
        var newWorkout = new Workout
        {
            Name = workout.Name
        };

        context.Attach(newWorkout);

        UpdateRoutine.Workouts.Add(newWorkout);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to save the new workout", ex);
        }
    }

    public async Task UpdateWorkoutAsync(UpdateWorkoutExerciseSetDto updateWorkout)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var workoutToUpdate = await context.Workouts
            .Include(x => x.ExerciseSets)
            .ThenInclude(x => x.Exercise)
            .FirstOrDefaultAsync(x => x.Id == updateWorkout.WorkoutId)
            ?? throw new Exception("Workout not found");

        workoutToUpdate.Name = updateWorkout.WorkoutName;

        List<ExerciseSet> updateExerciseSets = [];

        foreach (var exerciseSet in updateWorkout.ExerciseSets)
        {
            Exercise exercise = await context.Exercises.FindAsync(exerciseSet.Exercise.Id) ?? throw new Exception("Exercise not found");
            ExerciseSet newExerciseSet = new ExerciseSet
            {
                Id = exerciseSet.Id,
                Sequence = exerciseSet.Sequence,
                Exercise = exercise,
                Weight = exerciseSet.Weight,
                Repetitions = exerciseSet.Repetitions,
                SetCount = exerciseSet.SetCount,
                Workout = workoutToUpdate
            };

            updateExerciseSets.Add(newExerciseSet);
        }

        workoutToUpdate.ExerciseSets = updateExerciseSets;
        context.Entry(workoutToUpdate).CurrentValues.SetValues(updateExerciseSets);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to update workout", ex);
        }
    }

    // EXERCISE SETS
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
