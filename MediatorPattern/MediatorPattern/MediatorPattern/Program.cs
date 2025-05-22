using System;
using System.Collections.Generic;

public interface IChatMediator
{
    void SendMessage(string message, Participant sender);
    void RegisterParticipant(Participant participant);
}

public abstract class Participant
{
    protected IChatMediator _mediator;
    public string Name { get; }

    public Participant(string name, IChatMediator mediator)
    {
        Name = name;
        _mediator = mediator;
    }

    public void Send(string message)
    {
        Console.WriteLine($"{Name} sends: {message}");
        _mediator.SendMessage(message, this);
    }

    public abstract void Receive(string message, string from);
}

public class FitnessChatMediator : IChatMediator
{
    private List<Participant> _participants = new List<Participant>();

    public void RegisterParticipant(Participant participant)
    {
        _participants.Add(participant);
    }

    public void SendMessage(string message, Participant sender)
    {
        foreach (var p in _participants)
        {
            if (p != sender)
            {
                p.Receive(message, sender.Name);
            }
        }
    }
}

public class Coach : Participant
{
    public Coach(string name, IChatMediator mediator) : base(name, mediator) { }

    public override void Receive(string message, string from)
    {
        Console.WriteLine($"Coach {Name} received from {from}: {message}");
    }
}

public class Client : Participant
{
    public Client(string name, IChatMediator mediator) : base(name, mediator) { }

    public override void Receive(string message, string from)
    {
        Console.WriteLine($"Client {Name} received from {from}: {message}");
    }
}

class Program
{
    static void Main()
    {
        IChatMediator mediator = new FitnessChatMediator();

        Participant coach = new Coach("Alex", mediator);
        Participant client1 = new Client("Vadim", mediator);
        Participant client2 = new Client("Sofia", mediator);

        mediator.RegisterParticipant(coach);
        mediator.RegisterParticipant(client1);
        mediator.RegisterParticipant(client2);

        coach.Send("Welcome to today's full body workout!");
        client1.Send("Ready, Coach!");
        client2.Send("Let's go!");
    }
}