using System;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Generate the array
            int[] numbers = GenerateNumbers(10);

            // Step 2: Reverse the array in-place
            Reverse(numbers);

            // Step 3: Print the reversed array
            PrintNumbers(numbers);
        }

        // Method to generate an array of N numbers starting from 1 to N
        static int[] GenerateNumbers(int length)
        {
            int[] result = new int[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = i + 1;
            }
            return result;
        }

        // Method to reverse the array in-place using a temp variable
        static void Reverse(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n / 2; i++)
            {
                // Swap elements at index i and n - i - 1
                int temp = array[i];
                array[i] = array[n - i - 1];
                array[n - i - 1] = temp;
            }
        }

        // Method to print each number in the array
        static void PrintNumbers(int[] array)
        {
            foreach (int num in array)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine(); // Newline after printing the array
        }
    }
}