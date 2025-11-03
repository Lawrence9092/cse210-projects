using System;

using System;
using System.Collections.Generic;
using System.Linq; // for average and sorting functions

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>(); // create a list to store numbers

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int userNumber = -1; // initialize variable for input

        // keep looping until user enters 0
        while (userNumber != 0)
        {
            Console.Write("Enter number: ");
            userNumber = int.Parse(Console.ReadLine());

            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }
        }

        // --- Core Requirements ---
        // 1️⃣ Compute the sum
        int sum = numbers.Sum();

        // 2️⃣ Compute the average
        double average = numbers.Average();

        // 3️⃣ Find the largest number
        int maxNumber = numbers.Max();

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {maxNumber}");

        // --- Stretch Challenges ---
        if (numbers.Count > 0)
        {
            // Find the smallest positive number
            int smallestPositive = numbers
                .Where(n => n > 0)
                .DefaultIfEmpty(0)
                .Min();

            if (smallestPositive > 0)
                Console.WriteLine($"The smallest positive number is: {smallestPositive}");

            // Sort and display the list
            numbers.Sort();
            Console.WriteLine("The sorted list is:");
            foreach (int num in numbers)
            {
                Console.WriteLine(num);
            }
        }
    }
}
