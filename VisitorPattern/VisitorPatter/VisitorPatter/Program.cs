using System;
using System.Collections.Generic;

public interface IWorkoutVisitor
{
    void VisitCardio(CardioWorkout cardio);
    void VisitStrength(StrengthWorkout strength);
}

public interface IWorkout
{
    void Accept(IWorkoutVisitor visitor);
}

public class CardioWorkout : IWorkout
{
    public int DurationMinutes { get; }
    public string Type { get; }

    public CardioWorkout(string type, int duration)
    {
        Type = type;
        DurationMinutes = duration;
    }

    public void Accept(IWorkoutVisitor visitor)
    {
        visitor.VisitCardio(this);
    }
}

public class StrengthWorkout : IWorkout
{
    public int Sets { get; }
    public string MuscleGroup { get; }

    public StrengthWorkout(string muscleGroup, int sets)
    {
        MuscleGroup = muscleGroup;
        Sets = sets;
    }

    public void Accept(IWorkoutVisitor visitor)
    {
        visitor.VisitStrength(this);
    }
}

public class WorkoutReportVisitor : IWorkoutVisitor
{
    public void VisitCardio(CardioWorkout cardio)
    {
        Console.WriteLine($"[Cardio Report] Type: {cardio.Type}, Duration: {cardio.DurationMinutes} minutes");
    }

    public void VisitStrength(StrengthWorkout strength)
    {
        Console.WriteLine($"[Strength Report] Muscle Group: {strength.MuscleGroup}, Sets: {strength.Sets}");
    }
}

public class CalorieEstimatorVisitor : IWorkoutVisitor
{
    public void VisitCardio(CardioWorkout cardio)
    {
        int calories = cardio.DurationMinutes * 10;
        Console.WriteLine($"[Calories] {cardio.Type} burned approx. {calories} kcal.");
    }

    public void VisitStrength(StrengthWorkout strength)
    {
        int calories = strength.Sets * 15;
        Console.WriteLine($"[Calories] Strength workout for {strength.MuscleGroup} burned approx. {calories} kcal.");
    }
}

class Program
{
    static void Main()
    {
        List<IWorkout> workouts = new List<IWorkout>
        {
            new CardioWorkout("Running", 30),
            new StrengthWorkout("Chest", 5),
            new CardioWorkout("Cycling", 45),
            new StrengthWorkout("Legs", 4)
        };

        IWorkoutVisitor reportVisitor = new WorkoutReportVisitor();
        IWorkoutVisitor calorieVisitor = new CalorieEstimatorVisitor();

        Console.WriteLine("--- Workout Reports ---");
        foreach (var workout in workouts)
        {
            workout.Accept(reportVisitor);
        }

        Console.WriteLine("\n--- Calorie Estimates ---");
        foreach (var workout in workouts)
        {
            workout.Accept(calorieVisitor);
        }
    }
}