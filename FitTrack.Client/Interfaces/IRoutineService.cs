using FitTrack.Shared.DTOs.Exercises;
using FitTrack.Shared.DTOs.Routines;

namespace FitTrack.Client.Interfaces;

public interface IRoutineService
{
    Task CreateExerciseAsync(CreateExerciseDto newExercise);
    Task DeleteExerciseAsync(int exerciseId);
    Task UpdateExerciseAsync(ExerciseDto exercise);
    Task<List<ExerciseDto>> GetAllExercisesAsync();
    Task<List<RoutineDto>> GetAllRoutinesAsync();
    Task CreateRoutineAsync(CreateRoutineDto routine);
}
