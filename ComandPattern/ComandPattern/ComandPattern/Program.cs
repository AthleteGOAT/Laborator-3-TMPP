using System;
using System.Collections.Generic;

public interface ICommand
{
    void Execute();
    void Undo(); 
}

public class FitnessTrainer
{
    public void StartExercise(string exercise)
    {
        Console.WriteLine($"Starting {exercise}...");
    }

    public void StopExercise(string exercise)
    {
        Console.WriteLine($"Stopping {exercise}.");
    }

    public void TakeRest()
    {
        Console.WriteLine("Taking a rest...");
    }
}

public class StartExerciseCommand : ICommand
{
    private FitnessTrainer _trainer;
    private string _exercise;

    public StartExerciseCommand(FitnessTrainer trainer, string exercise)
    {
        _trainer = trainer;
        _exercise = exercise;
    }

    public void Execute()
    {
        _trainer.StartExercise(_exercise);
    }

    public void Undo()
    {
        _trainer.StopExercise(_exercise);
    }
}

public class RestCommand : ICommand
{
    private FitnessTrainer _trainer;

    public RestCommand(FitnessTrainer trainer)
    {
        _trainer = trainer;
    }

    public void Execute()
    {
        _trainer.TakeRest();
    }

    public void Undo()
    {
        Console.WriteLine("Rest undone (back to workout)!");
    }
}

public class WorkoutInvoker
{
    private List<ICommand> _commandHistory = new List<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commandHistory.Add(command);
    }

    public void UndoLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            var lastCommand = _commandHistory[^1];
            lastCommand.Undo();
            _commandHistory.RemoveAt(_commandHistory.Count - 1);
        }
        else
        {
            Console.WriteLine("No command to undo.");
        }
    }
}


class Program
{
    static void Main()
    {
        FitnessTrainer trainer = new FitnessTrainer();
        WorkoutInvoker workout = new WorkoutInvoker();

        ICommand pushups = new StartExerciseCommand(trainer, "Push-ups");
        ICommand rest = new RestCommand(trainer);

        workout.ExecuteCommand(pushups); 
        workout.ExecuteCommand(rest);    

        workout.UndoLastCommand();       
        workout.UndoLastCommand();       
    }
}