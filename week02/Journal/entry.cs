using System;

public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    // Display a single journal entry
    public void Display()
    {
        Console.WriteLine($"\nDate: {Date}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}");
        Console.WriteLine("--------------------------------------");
    }

    // Convert entry to text for file saving
    public string ToFileFormat()
    {
        return $"{Date}|{Prompt}|{Response}";
    }

    // Convert text line from file back into an Entry object
    public static Entry FromFileFormat(string line)
    {
        string[] parts = line.Split('|');
        if (parts.Length >= 3)
        {
            return new Entry(parts[0], parts[1], parts[2]);
        }
        return null;
    }
}
