using System;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;

public class Fraction
{
    private int _numerator;
    private int _denominator;

    // Constructor with no parameters (defaults to 1/1)
    public Fraction()
    {
        _numerator = 1;
        _denominator = 1;
    }

    // Constructor with one parameter (defaults the denominator to 1)
    public Fraction(int numerator)
    {
        _numerator = numerator;
        _denominator = 1;
    }

    // Constructor with two parameters (for custom fraction)
    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        _numerator = numerator;
        _denominator = denominator;
    }

    // Getter for numerator
    public int GetNumerator()
    {
        return _numerator;
    }

    // Setter for numerator
    public void SetNumerator(int numerator)
    {
        _numerator = numerator;
    }

    // Getter for denominator
    public int GetDenominator()
    {
        return _denominator;
    }

    // Setter for denominator with validation (cannot be zero)
    public void SetDenominator(int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be 0");
        }
        _denominator = denominator;
    }

    // Method to get the fraction as a string (e.g., "3/4")
    public string GetFractionString()
    {
        return $"{_numerator}/{_denominator}";
    }

    // Method to get the decimal value (e.g., 0.75 for 3/4)
    public double GetDecimalValue()
    {
        return (double)_numerator / _denominator;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Using different constructors
        Fraction fraction1 = new Fraction();        // Defaults to 1/1
        Fraction fraction2 = new Fraction(6);       // Defaults to 6/1
        Fraction fraction3 = new Fraction(6, 7);    // Custom fraction 6/7

        // Display the fractions as strings
        Console.WriteLine("Fraction 1: " + fraction1.GetFractionString()); // Output: 1/1
        Console.WriteLine("Fraction 2: " + fraction2.GetFractionString()); // Output: 6/1
        Console.WriteLine("Fraction 3: " + fraction3.GetFractionString()); // Output: 6/7

        // Display the decimal values
        Console.WriteLine("Fraction 1 decimal: " + fraction1.GetDecimalValue()); // Output: 1.0
        Console.WriteLine("Fraction 2 decimal: " + fraction2.GetDecimalValue()); // Output: 6.0
        Console.WriteLine("Fraction 3 decimal: " + fraction3.GetDecimalValue()); // Output: 0.8571

        // Using setters to modify fraction values
        fraction1.SetNumerator(3);
        fraction1.SetDenominator(4);
        Console.WriteLine("Updated Fraction 1: " + fraction1.GetFractionString()); // Output: 3/4
        Console.WriteLine("Updated Fraction 1 decimal: " + fraction1.GetDecimalValue()); // Output: 0.75
    }
}
