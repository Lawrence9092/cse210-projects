using System;

using System;

class Program
{
    static void Main(string[] args)
    {
        // Call each function in order
        DisplayWelcome();

        string userName = PromptUserName();
        int userNumber = PromptUserNumber();
        int squaredNumber = SquareNumber(userNumber);

        DisplayResult(userName, squaredNumber);
    }

    // 1️⃣ Displays a welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // 2️⃣ Prompts user for their name and returns it
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // 3️⃣ Prompts user for their favorite number and returns it as integer
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }

    // 4️⃣ Accepts an integer and returns its square
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // 5️⃣ Displays the user's name and squared number
    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}
