using System;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Mindfulness Program
/// - Base Activity class with shared behaviors (start/end message, spinner, countdown)
/// - BreathingActivity, ReflectionActivity, ListingActivity implement specific behavior
/// - Single-file console application ready to run
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        MindfulnessApp app = new MindfulnessApp();
        app.Run();
    }
}

/// <summary>
/// Application controller: shows menu and dispatches activities
/// </summary>
public class MindfulnessApp
{
    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Program ===\n");
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("\nEnter choice (1-4): ");

            string choice = Console.ReadLine()?.Trim();
            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    running = false;
                    continue;
                default:
                    Console.WriteLine("Invalid choice. Press any key to continue...");
                    Console.ReadKey();
                    continue;
            }

            activity.Start(); // runs whole activity flow
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        Console.WriteLine("\nThank you for using the Mindfulness Program. Goodbye!");
    }
}

/// <summary>
/// Base Activity class with shared behaviors and utilities.
/// Derived classes implement RunActivity() to provide specific behavior.
/// </summary>
public abstract class Activity
{
    // Encapsulated fields
    private string _name;
    private string _description;
    private int _durationSeconds = 0;

    // Constructor for derived classes to set name/description
    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // Public starter used by the application
    public void Start()
    {
        Console.Clear();
        DisplayStartMessage();
        _durationSeconds = PromptForDuration();
        PrepareToBegin();
        // Run the specific activity implementation
        RunActivity(_durationSeconds);
        DisplayEndMessage();
    }

    // Derived classes must implement this to define their behaviour
    protected abstract void RunActivity(int durationSeconds);

    // Shared: display the start block
    private void DisplayStartMessage()
    {
        Console.WriteLine($"--- {_name} ---\n");
        Console.WriteLine(_description + "\n");
    }

    // Shared: ask and return duration (in seconds)
    private int PromptForDuration()
    {
        int seconds = 0;
        while (true)
        {
            Console.Write("Enter the duration in seconds for this activity (e.g. 30): ");
            string input = Console.ReadLine()?.Trim();
            if (int.TryParse(input, out seconds) && seconds > 0)
                break;
            Console.WriteLine("Please enter a valid positive integer for seconds.");
        }
        return seconds;
    }

    // Shared prepare: short pause with spinner
    private void PrepareToBegin()
    {
        Console.WriteLine("\nGet ready...");
        ShowSpinner(3); // 3-second prepare spinner
    }

    // Shared ending message: show completed message and pause
    private void DisplayEndMessage()
    {
        Console.WriteLine("\nWell done!");
        ShowSpinner(2);
        Console.WriteLine($"You have completed the {_name} for { _durationSeconds } seconds.");
        ShowSpinner(2);
    }

    // Shared animation: spinner for n seconds
    protected void ShowSpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };
        DateTime end = DateTime.Now.AddSeconds(seconds);
        int i = 0;
        while (DateTime.Now < end)
        {
            Console.Write(frames[i % frames.Length]);
            Thread.Sleep(250);
            Console.Write("\b");
            i++;
        }
    }

    // Shared animation: countdown from n seconds (prints numbers)
    protected void ShowCountdown(int seconds)
    {
        for (int s = seconds; s >= 1; s--)
        {
            Console.Write(s);
            Thread.Sleep(1000);
            Console.Write("\b \b"); // erase the digit(s)
        }
    }
}

/// <summary>
/// Breathing Activity: alternates 'Breathe in...' and 'Breathe out...' with countdowns until total duration elapses
/// </summary>
public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity",
               "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    { }

    protected override void RunActivity(int durationSeconds)
    {
        Console.WriteLine("\nWe will begin breathing cycles now.");
        Console.WriteLine("Focus and follow the prompts.\n");

        DateTime start = DateTime.Now;
        DateTime endTime = start.AddSeconds(durationSeconds);

        // We'll do alternating cycles. For each cycle we'll breathe in for 4 sec then breathe out for 6 sec (example)
        int inhale = 4;
        int exhale = 6;

        while (DateTime.Now < endTime)
        {
            Console.Write("\nBreathe in... ");
            ShowCountdown(Math.Min(inhale, SecondsRemaining(endTime)));
            if (DateTime.Now >= endTime) break;

            Console.Write("\nBreathe out... ");
            ShowCountdown(Math.Min(exhale, SecondsRemaining(endTime)));
            // small spinner between cycles
            if (DateTime.Now < endTime)
            {
                ShowSpinner(1);
            }
        }
        Console.WriteLine();
    }

    private int SecondsRemaining(DateTime endTime)
    {
        int seconds = (int)Math.Ceiling((endTime - DateTime.Now).TotalSeconds);
        return Math.Max(0, seconds);
    }
}

/// <summary>
/// Reflection Activity: shows a random prompt then shows questions (randomly) and pauses between them.
/// </summary>
public class ReflectionActivity : Activity
{
    private static readonly string[] Prompts = new string[]
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly string[] Questions = new string[]
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    private Random _random = new Random();

    public ReflectionActivity()
        : base("Reflection Activity",
               "This activity will help you reflect on times in your life when you have shown strength and resilience. " +
               "This will help you recognize the power you have and how you can use it in other aspects of your life.")
    { }

    protected override void RunActivity(int durationSeconds)
    {
        Console.WriteLine("\nReflect on the prompt below:\n");
        string prompt = Prompts[_random.Next(Prompts.Length)];
        Console.WriteLine($"--- {prompt} ---\n");

        Console.WriteLine("When you have something in mind, press Enter to continue.");
        Console.ReadLine();

        DateTime start = DateTime.Now;
        DateTime endTime = start.AddSeconds(durationSeconds);

        // Ask random questions until time elapses
        while (DateTime.Now < endTime)
        {
            // pick a random question
            string q = Questions[_random.Next(Questions.Length)];
            Console.WriteLine("\n" + q);
            // pause while showing spinner for 6 seconds or until time end
            int pause = Math.Min(6, SecondsRemaining(endTime));
            if (pause <= 0) break;
            ShowSpinner(pause);
        }
    }

    private int SecondsRemaining(DateTime endTime)
    {
        int seconds = (int)Math.Ceiling((endTime - DateTime.Now).TotalSeconds);
        return Math.Max(0, seconds);
    }
}

/// <summary>
/// Listing Activity: shows a prompt, gives user a short countdown to begin, then collects typed items until time runs out.
/// Uses Console.KeyAvailable to avoid blocking reads so we can stop when time is up.
/// </summary>
public class ListingActivity : Activity
{
    private static readonly string[] Prompts = new string[]
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private Random _random = new Random();

    public ListingActivity()
        : base("Listing Activity",
               "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    { }

    protected override void RunActivity(int durationSeconds)
    {
        string prompt = Prompts[_random.Next(Prompts.Length)];
        Console.WriteLine("\nYour prompt:");
        Console.WriteLine($"--- {prompt} ---\n");

        // give a short countdown before starting listing
        Console.WriteLine("You will have a short moment to think; then start listing items. Get ready...");
        ShowCountdown(5);
        Console.WriteLine("\nStart listing! (press Enter after each item)");

        List<string> entries = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(durationSeconds);

        // Read items until time is up. We'll use KeyAvailable to check whether user typed something and use ReadLine only when a key is available
        while (DateTime.Now < endTime)
        {
            // if there's no input available, sleep a short while and continue
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(100); // reduce CPU usage
                continue;
            }

            // when a key is available, safely read the line
            string line = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(line))
            {
                entries.Add(line.Trim());
            }
        }

        // After time elapses show number of items
        Console.WriteLine($"\nTime's up! You entered {entries.Count} item(s).");

        if (entries.Count > 0)
        {
            Console.WriteLine("Here are your entries:");
            foreach (var e in entries)
            {
                Console.WriteLine($"- {e}");
            }
        }
    }
}

