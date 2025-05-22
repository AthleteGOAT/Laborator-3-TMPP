using System;
using System.Collections.Generic;


public interface IObserver
{
    void Update(string workout);
}

public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}

public class FitnessApp : ISubject
{
    private List<IObserver> _observers = new List<IObserver>();
    private string _latestWorkout;

    public void AddNewWorkout(string workout)
    {
        _latestWorkout = workout;
        Console.WriteLine($"\n[FitnessApp] New workout added: {workout}");
        Notify();
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
        Console.WriteLine("[FitnessApp] A new user subscribed.");
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
        Console.WriteLine("[FitnessApp] A user unsubscribed.");
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_latestWorkout);
        }
    }
}

public class User : IObserver
{
    private string _name;

    public User(string name)
    {
        _name = name;
    }

    public void Update(string workout)
    {
        Console.WriteLine($"{_name} received notification: New workout - {workout}");
    }
}

class Program
{
    static void Main()
    {
        FitnessApp app = new FitnessApp();

        User alice = new User("Alice");
        User bob = new User("Bob");
        User charlie = new User("Charlie");

        app.Attach(alice);
        app.Attach(bob);

        app.AddNewWorkout("HIIT Cardio - 20 minutes");

        app.Attach(charlie);
        app.AddNewWorkout("Full Body Strength - 45 minutes");

        app.Detach(bob);
        app.AddNewWorkout("Stretch & Relax - 15 minutes");
    }
}