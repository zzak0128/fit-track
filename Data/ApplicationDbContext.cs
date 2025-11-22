using FitTrack.Data.Models.Activities;
using FitTrack.Data.Models.FoodJournal;
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

    public DbSet<ActivitySet> ActivitySets { get; set; }

    public DbSet<Measurement> Measurements { get; set; }

    public DbSet<MeasurementData> MeasurementData { get; set; }

    public DbSet<ExerciseImage> ExerciseImages { get; set; }

    public DbSet<FoodItem> FoodItems { get; set; }

    public DbSet<MealFoodServing> MealFoodServings { get; set; }

    public DbSet<Meal> Meals { get; set; }

    public DbSet<UserGoals> UserGoals { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Workout>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany(w => w.ExerciseSets)
            .WithOne(e => e.Workout)
            .OnDelete(DeleteBehavior.Cascade);

            e.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();
        });

        modelBuilder.Entity<Routine>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany(r => r.Workouts)
            .WithOne(w => w.Routine)
            .OnDelete(DeleteBehavior.Cascade);

            e.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

            e.Property(x => x.Description)
            .HasMaxLength(300)
            .IsRequired(false);
        });

        modelBuilder.Entity<ExerciseSet>(e =>
        {
            e.HasKey(e => e.Id);
            e.HasOne(e => e.Exercise)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

            e.Property(x => x.Sequence)
            .HasDefaultValue(1);
        });

        modelBuilder.Entity<Exercise>(e =>
        {
            e.HasKey(e => e.Id);
            e.HasMany(e => e.ExerciseSet)
            .WithOne(x => x.Exercise)
            .OnDelete(DeleteBehavior.Cascade);

            e.HasMany(e => e.Thumbnails)
            .WithOne(x => x.Exercise)
            .OnDelete(DeleteBehavior.Cascade);

            e.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

            e.Property(x => x.Description)
            .IsRequired(false);
        });


        modelBuilder.Entity<Activity>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany(e => e.WorkoutLogs)
            .WithOne(x => x.Activity)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WorkoutLog>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany(x => x.ActivitySets)
            .WithOne(x => x.WorkoutLog)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ActivitySet>(e =>
        {
            e.HasKey(x => x.Id);
        });

        modelBuilder.Entity<Measurement>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany(e => e.MeasurementData)
            .WithOne(m => m.Measurement)
            .OnDelete(DeleteBehavior.Cascade);

            e.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        });

        modelBuilder.Entity<MeasurementData>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Unit)
            .HasMaxLength(10)
            .IsRequired();

            e.Property(x => x.Amount)
            .HasPrecision(5, 2)
            .IsRequired();
        });

        modelBuilder.Entity<Meal>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasMany(m => m.Foods)
            .WithMany(f => f.Meals);
        });

        modelBuilder.Entity<MealFoodServing>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(m => m.FoodItem)
            .WithMany(f => f.MealFoodServings);
        });

        modelBuilder.Entity<FoodItem>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name)
            .HasMaxLength(250)
            .IsRequired();
            e.Property(x => x.Units)
            .HasMaxLength(50);
        });

        modelBuilder.Entity<UserGoals>(e =>
        {
            e.HasKey(x => x.Id);
        });
    }
}
