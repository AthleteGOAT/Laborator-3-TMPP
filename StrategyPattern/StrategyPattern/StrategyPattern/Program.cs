using System;

public interface ITrainingStrategy
{
    void Train();
}

public class StrengthTraining : ITrainingStrategy
{
    public void Train()
    {
        Console.WriteLine("Training for strength: Squats, Deadlifts, Bench Press.");
    }
}

public class CardioTraining : ITrainingStrategy
{
    public void Train()
    {
        Console.WriteLine("Doing cardio training: Running, Cycling, Rowing.");
    }
}

public class FlexibilityTraining : ITrainingStrategy
{
    public void Train()
    {
        Console.WriteLine("Improving flexibility: Yoga, Stretching, Mobility drills.");
    }
}

public class FitnessClient
{
    private ITrainingStrategy _strategy;

    public FitnessClient(ITrainingStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(ITrainingStrategy strategy)
    {
        _strategy = strategy;
    }

    public void StartTraining()
    {
        _strategy.Train();
    }
}

class Program
{
    static void Main()
    {
        FitnessClient client = new FitnessClient(new StrengthTraining());
        client.StartTraining();  

        client.SetStrategy(new CardioTraining());
        client.StartTraining(); 

        client.SetStrategy(new FlexibilityTraining());
        client.StartTraining();  
    }
}