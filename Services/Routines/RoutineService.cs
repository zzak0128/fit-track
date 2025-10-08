using FitTrack.Data;
using FitTrack.Data.DTOs.Exercises;
using FitTrack.Data.DTOs.Routines;
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
    public async Task<List<BaseRoutineDto>> GetBaseRoutinesAsync(ApplicationUser user)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Routines
            .Include(r => r.Workouts)
            .ThenInclude(w => w.ExerciseSets)
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
}
