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
    Task UpdateRoutineAsync(UpdateRoutineWorkoutsDto routine);
    Task<List<BaseWorkoutDto>> GetRoutineWorkoutsAsync(int routineId);
    Task CreateWorkoutAsync(CreateWorkoutDto workout);
    Task UpdateWorkoutAsync(UpdateWorkoutExerciseSetDto updateWorkout);
    Task<List<ExerciseSetDto>> GetAllExerciseSetsAsync(int WorkoutId);
}
