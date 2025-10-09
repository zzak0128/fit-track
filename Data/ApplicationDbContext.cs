using FitTrack.Data.Models.Activities;
using FitTrack.Data.Models.Measurements;
using FitTrack.Data.Models.Routines;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Workout> Workouts { get; set; }

    public DbSet<Routine> Routines { get; set; }

    public DbSet<ExerciseSet> ExerciseSets { get; set; }

    public DbSet<Exercise> Exercises { get; set; }

    public DbSet<Activity> Activities { get; set; }

    public DbSet<WorkoutLog> WorkoutLogs { get; set; }

    public DbSet<Measurement> Measurements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Workout>()
            .HasMany(w => w.ExerciseSets)
            .WithOne(e => e.Workout)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Routine>()
            .HasMany(r => r.Workouts)
            .WithOne(w => w.Routine)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ExerciseSet>()
            .HasOne(e => e.Exercise)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Exercise>()
            .HasMany(e => e.ExerciseSet)
            .WithOne(x => x.Exercise)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Activity>()
            .HasOne(e => e.Workout)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Measurement>()
            .HasMany(e => e.MeasurementData)
            .WithOne(m => m.Measurement)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
