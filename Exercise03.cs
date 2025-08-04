using System;

namespace Chapter03
{
    public class Exercise03
    {
        // Runs all the exercises in order
        public static void RunAll()
        {
            Console.WriteLine("Running Chapter 03 Exercises...\n");

            RunFizzBuzz();
            Console.WriteLine("\n---");

            RunByteOverflowExample();
            Console.WriteLine("\n---");

            RunNumberGuessingGame();
            Console.WriteLine("\n---");

            RunPyramid();
            Console.WriteLine("\n---");

            RunAgeInDays();
            Console.WriteLine("\n---");

            RunTimeBasedGreeting();
            Console.WriteLine("\n---");

            RunStepCounter();
        }

        // 1. FizzBuzz: Prints numbers from 1 to 100
        // Replaces numbers divisible by 3 with "fizz", by 5 with "buzz", and by both with "fizzbuzz"
        public static void RunFizzBuzz()
        {
            Console.WriteLine("FizzBuzz up to 100:");
            for (int i = 1; i <= 100; i++)
            {
                if (i % 15 == 0) Console.WriteLine("fizzbuzz");
                else if (i % 3 == 0) Console.WriteLine("fizz");
                else if (i % 5 == 0) Console.WriteLine("buzz");
                else Console.WriteLine(i);
            }
        }

        // 2. Demonstrates what happens when you use a byte in a loop that exceeds its max value (255)
        public static void RunByteOverflowExample()
        {
            Console.WriteLine("Byte overflow test:");
            int max = 500;

            // Warns if max exceeds byte capacity
            if (max > byte.MaxValue)
                Console.WriteLine("⚠ Warning: max exceeds byte limit. 'i' will overflow!");

            // This loop would overflow without the break at 255
            for (byte i = 0; i < max; i++)
            {
                Console.WriteLine(i);
                if (i == 255) break; // Prevent infinite loop due to overflow
            }
        }

        // 3. Number guessing game: Random number between 1 and 3
        // User guesses and gets feedback: too low, too high, correct, or out of range
        public static void RunNumberGuessingGame()
        {
            int correctNumber = new Random().Next(1, 4); // Generates 1, 2, or 3

            Console.Write("Guess a number between 1 and 3: ");
            int guessedNumber = int.Parse(Console.ReadLine());

            if (guessedNumber < 1 || guessedNumber > 3)
                Console.WriteLine("Your guess is out of range!");
            else if (guessedNumber < correctNumber)
                Console.WriteLine("Too low!");
            else if (guessedNumber > correctNumber)
                Console.WriteLine("Too high!");
            else
                Console.WriteLine("Correct!");
        }

        // 4. Pyramid pattern: Prints 5 rows of centered stars to form a pyramid
        public static void RunPyramid()
        {
            Console.WriteLine("Pyramid pattern:");
            int rows = 5;

            for (int i = 1; i <= rows; i++)
            {
                // Print spaces
                for (int space = 1; space <= rows - i; space++)
                    Console.Write(" ");

                // Print stars
                for (int star = 1; star <= (2 * i - 1); star++)
                    Console.Write("*");

                Console.WriteLine(); // New line after each row
            }
        }

        // 5. Calculates age in days based on a fixed birth date
        // Also shows the next 10,000-day anniversary date
        public static void RunAgeInDays()
        {
            DateTime birthDate = new DateTime(2000, 1, 1); // Change as needed
            DateTime today = DateTime.Now;

            int ageInDays = (int)(today - birthDate).TotalDays;
            int daysToNextAnniversary = 10000 - (ageInDays % 10000);
            DateTime nextAnniversary = today.AddDays(daysToNextAnniversary);

            Console.WriteLine($"You are {ageInDays} days old.");
            Console.WriteLine($"Your next 10,000-day anniversary is on {nextAnniversary.ToShortDateString()}.");
        }

        // 6. Greets the user based on the current time of day
        // Uses only if statements (no else or switch)
        public static void RunTimeBasedGreeting()
        {
            DateTime now = DateTime.Now;
            int hour = now.Hour;

            if (hour >= 5 && hour < 12)
                Console.WriteLine("Good Morning");
            if (hour >= 12 && hour < 17)
                Console.WriteLine("Good Afternoon");
            if (hour >= 17 && hour < 21)
                Console.WriteLine("Good Evening");
            if (hour >= 21 || hour < 5)
                Console.WriteLine("Good Night");
        }

        // 7. Nested loop that prints numbers from 0 to 24 in steps of 1, 2, 3, and 4
        public static void RunStepCounter()
        {
            Console.WriteLine("Counting up to 24 using different steps:");

            for (int step = 1; step <= 4; step++) // Step values: 1, 2, 3, 4
            {
                for (int i = 0; i <= 24; i += step)
                {
                    Console.Write(i);
                    if (i + step <= 24) Console.Write(",");
                }
                Console.WriteLine(); // New line after each step series
            }
        }
    }
}
