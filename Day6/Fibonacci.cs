// Part 1: Fibonacci Sequence using Recursion
using System;

namespace FibonacciApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("First 10 Fibonacci Numbers:");
            for (int i = 1; i <= 10; i++)
            {
                Console.Write(Fibonacci(i) + " ");
            }
            Console.WriteLine();
        }

        // Recursive method to calculate Fibonacci number
        static int Fibonacci(int n)
        {
            if (n <= 0) return 0;
            if (n == 1 || n == 2) return 1;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }
}


