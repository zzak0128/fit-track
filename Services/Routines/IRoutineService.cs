using FitTrack.Data;
using FitTrack.Data.DTOs.Exercises;
using FitTrack.Data.DTOs.Routines;

namespace FitTrack.Services.Routines;

public interface IRoutineService
{
    Task CreateExerciseAsync(CreateExerciseDto newExercise);
    Task DeleteExerciseAsync(int exerciseId);
    Task UpdateExerciseAsync(ExerciseDto exercise);
    Task<List<ExerciseDto>> GetAllExercisesAsync();
    Task<List<BaseRoutineDto>> GetBaseRoutinesAsync(ApplicationUser user);
    Task CreateRoutineAsync(CreateRoutineDto routine);
}
