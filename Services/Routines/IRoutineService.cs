using FitTrack.Data;
using FitTrack.Data.DTOs.Exercises;
using FitTrack.Data.DTOs.ExerciseSets;
using FitTrack.Data.DTOs.Routines;
using FitTrack.Data.DTOs.Workouts;

namespace FitTrack.Services.Routines;

public interface IRoutineService
{
    Task CreateExerciseAsync(CreateExerciseDto newExercise);
    Task DeleteExerciseAsync(int exerciseId);
    Task UpdateExerciseAsync(ExerciseDto exercise);
    Task<List<ExerciseDto>> GetAllExercisesAsync();
    Task<List<BaseRoutineDto>> GetBaseRoutinesAsync(ApplicationUser user);
    Task CreateRoutineAsync(CreateRoutineDto routine);
    Task DeleteRoutineAsync(int routineId);
    Task<List<BaseWorkoutDto>> GetRoutineWorkoutsAsync(int routineId);
    Task<List<ExerciseSetDto>> GetAllExerciseSetsAsync(int WorkoutId);
    Task<DetailRoutineDto> GetDetailRoutineByIdAsync(int routineId, ApplicationUser user);
    Task AddWorkoutToRoutineAsync(int routineId, BaseWorkoutDto newWorkout);
    Task AddExerciseSetToWorkoutAsync(CreateExerciseSetDto newSet);
    Task UpdateExerciseSetAsync(UpdateExerciseSetDto updateExerciseSet);
    Task RemoveExerciseSetAsync(RemoveExerciseSetDto removeExerciseSet);
    Task RemoveWorkoutAsync(int workoutId);
    Task CreateExerciseAsync(List<ExerciseDto> exercises);
    Task<string> RenameWorkoutAsync(RenameWorkoutDto workout);
}
