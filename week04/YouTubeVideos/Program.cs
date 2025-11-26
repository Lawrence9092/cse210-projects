using System;


public class Comment
{
    private string _commenterName;
    private string _text;

    public Comment(string commenterName, string text)
    {
        _commenterName = commenterName;
        _text = text;
    }

    public void Display()
    {
        Console.WriteLine($"- {_commenterName}: {_text}");
    }
}

public class Video
{
    private string _title;
    private string _author;
    private int _lengthSeconds;
    private List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, int lengthSeconds)
    {
        _title = title;
        _author = author;
        _lengthSeconds = lengthSeconds;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public void Display()
    {
        Console.WriteLine("========================================");
        Console.WriteLine($"Title : {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_lengthSeconds} seconds");
        Console.WriteLine($"Comments ({GetCommentCount()}):");

        foreach (var comment in _comments)
        {
            comment.Display();
        }

        Console.WriteLine("========================================");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== YouTube Video Abstraction Program ===\n");

        // ---------------------------------------
        // Create Video 1
        // ---------------------------------------
        Video v1 = new Video("Understanding Abstraction", "CS Academy", 480);
        v1.AddComment(new Comment("Alice", "This helped me a lot!"));
        v1.AddComment(new Comment("Bob", "Can you make one about inheritance?"));

        // ---------------------------------------
        // Create Video 2
        // ---------------------------------------
        Video v2 = new Video("SOLID Principles", "Dev Mastery", 600);
        v2.AddComment(new Comment("David", "Very clear explanation."));
        v2.AddComment(new Comment("Eve", "More examples please!"));
        v2.AddComment(new Comment("Frank", "Loved the diagrams."));

        // ---------------------------------------
        // Create Video 3
        // ---------------------------------------
        Video v3 = new Video("C# Encapsulation Example", "CodeWithMe", 300);
        v3.AddComment(new Comment("Grace", "Short and helpful!"));

        // ---------------------------------------
        // Display all videos
        // ---------------------------------------
        List<Video> videos = new List<Video> { v1, v2, v3 };

        foreach (var video in videos)
        {
            video.Display();
        }

        Console.WriteLine("\nProgram Completed.");
    }
}
