using FitTrack.Client.Interfaces;
using FitTrack.Data;
using FitTrack.Models.Routines;
using FitTrack.Shared.DTOs.Exercises;
using FitTrack.Shared.DTOs.ExerciseSets;
using FitTrack.Shared.DTOs.Routines;
using FitTrack.Shared.DTOs.Workouts;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Services;

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
    public async Task<List<RoutineDto>> GetAllRoutinesAsync()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Routines
            .Include(r => r.Workouts)
            .ThenInclude(w => w.ExerciseSets)
            .Select(x => new RoutineDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                User = x.User,
                Workouts = x.Workouts
                .Select(w => new WorkoutDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    User = x.User,
                    ExerciseSets = w.ExerciseSets.Select(es => new ExerciseSetDto
                    {
                        Id = es.Id,
                        Repetitions = es.Repetitions,
                        SetCount = es.SetCount,
                        Weight = es.Weight,
                        Exercise = new ExerciseDto
                        {
                            Id = es.Exercise.Id,
                            Name = es.Exercise.Name,
                            Description = es.Exercise.Description,
                            MuscleGroup = es.Exercise.MuscleGroup
                        }
                    }).ToList()
                }).ToList(),
            }).ToListAsync();
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
