using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    // Add a new entry to the journal
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    // Display all journal entries
    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nNo entries found in your journal yet.");
        }
        else
        {
            Console.WriteLine("\n=== Your Journal Entries ===");
            foreach (Entry entry in _entries)
            {
                entry.Display();
            }
        }
    }

    // Save all entries to a file
    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.ToFileFormat());
            }
        }
        Console.WriteLine($"Journal saved to {fileName}");
    }

    // Load entries from a file
    public void LoadFromFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            _entries.Clear();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                Entry entry = Entry.FromFileFormat(line);
                if (entry != null)
                {
                    _entries.Add(entry);
                }
            }
            Console.WriteLine($"Journal loaded from {fileName}");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}
