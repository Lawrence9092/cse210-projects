using System;

using System;
using System.Collections.Generic;
using System.IO;

//
// Eternal Quest – Full Program
// Put this entire file into Program.cs
//

// =============================
// BASE CLASS
// =============================
public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;

    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDisplayText();
    public abstract string GetSaveData();
}

// =============================
// SIMPLE GOAL (one-time)
// =============================
public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override int RecordEvent()
    {
        _isComplete = true;
        return Points;
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDisplayText()
    {
        return $"{(IsComplete() ? "[X]" : "[ ]")} {Name} ({Description})";
    }

    public override string GetSaveData()
    {
        return $"SimpleGoal|{Name}|{Description}|{Points}|{_isComplete}";
    }
}

// =============================
// ETERNAL GOAL (never complete)
// =============================
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) {}

    public override int RecordEvent()
    {
        return Points;
    }

    public override bool IsComplete() => false;

    public override string GetDisplayText()
    {
        return $"[∞] {Name} ({Description})";
    }

    public override string GetSaveData()
    {
        return $"EternalGoal|{Name}|{Description}|{Points}";
    }
}

// =============================
// CHECKLIST GOAL (needs X repetitions)
// =============================
public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _targetCount = target;
        _bonus = bonus;
        _currentCount = 0;
    }

    public override int RecordEvent()
    {
        _currentCount++;

        if (_currentCount == _targetCount)
            return Points + _bonus;

        return Points;
    }

    public override bool IsComplete()
    {
        return _currentCount >= _targetCount;
    }

    public override string GetDisplayText()
    {
        return $"{(IsComplete() ? "[X]" : "[ ]")} {Name} ({Description}) — Completed {_currentCount}/{_targetCount}";
    }

    public override string GetSaveData()
    {
        return $"ChecklistGoal|{Name}|{Description}|{Points}|{_bonus}|{_targetCount}|{_currentCount}";
    }
}

// =============================
// GOAL MANAGER
// =============================
public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void DisplayGoals()
    {
        int i = 1;
        foreach (Goal g in _goals)
        {
            Console.WriteLine($"{i}. {g.GetDisplayText()}");
            i++;
        }
    }

    public void RecordEvent(int index)
    {
        int earned = _goals[index - 1].RecordEvent();
        _score += earned;
        Console.WriteLine($"You earned {earned} points!");
    }

    public void ShowScore()
    {
        Console.WriteLine($"\nTotal Score: {_score}\n");
    }

    // SAVE TO FILE
    public void Save(string fileName)
    {
        using (StreamWriter output = new StreamWriter(fileName))
        {
            output.WriteLine(_score);
            foreach (Goal g in _goals)
            {
                output.WriteLine(g.GetSaveData());
            }
        }
    }

    // LOAD FROM FILE
    public void Load(string fileName)
    {
        _goals.Clear();
        string[] lines = File.ReadAllLines(fileName);

        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split("|");
            string type = parts[0];

            if (type == "SimpleGoal")
            {
                SimpleGoal g = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                if (bool.Parse(parts[4])) g.RecordEvent();
                _goals.Add(g);
            }
            else if (type == "EternalGoal")
            {
                _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
            }
            else if (type == "ChecklistGoal")
            {
                ChecklistGoal g = new ChecklistGoal(
                    parts[1], parts[2], int.Parse(parts[3]),
                    int.Parse(parts[5]), int.Parse(parts[4])
                );

                int count = int.Parse(parts[6]);
                for (int c = 0; c < count; c++)
                {
                    g.RecordEvent();
                }

                _goals.Add(g);
            }
        }
    }
}

// =============================
// MAIN PROGRAM
// =============================
class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        int choice = 0;

        while (choice != 6)
        {
            Console.WriteLine("======= Eternal Quest =======");
            manager.ShowScore();

            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Quit");

            Console.Write("Choose an option: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateGoal(manager);
                    break;

                case 2:
                    manager.DisplayGoals();
                    break;

                case 3:
                    manager.DisplayGoals();
                    Console.Write("Which goal did you accomplish? ");
                    manager.RecordEvent(int.Parse(Console.ReadLine()));
                    break;

                case 4:
                    Console.Write("Filename: ");
                    manager.Save(Console.ReadLine());
                    break;

                case 5:
                    Console.Write("Filename: ");
                    manager.Load(Console.ReadLine());
                    break;
            }
        }
    }

    private static void CreateGoal(GoalManager manager)
    {
        Console.WriteLine("\nGoal Types:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Choose: ");

        int type = int.Parse(Console.ReadLine());

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        if (type == 1)
        {
            manager.AddGoal(new SimpleGoal(name, description, points));
        }
        else if (type == 2)
        {
            manager.AddGoal(new EternalGoal(name, description, points));
        }
        else if (type == 3)
        {
            Console.Write("Target number of repetitions: ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("Bonus for completion: ");
            int bonus = int.Parse(Console.ReadLine());

            manager.AddGoal(new ChecklistGoal(name, description, points, target, bonus));
        }

        Console.WriteLine("Goal created!\n");
    }
}

