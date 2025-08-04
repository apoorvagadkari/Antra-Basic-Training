using System;
using System.Runtime.InteropServices;

namespace _02UnderstandingTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1: Display type info
            Console.WriteLine("Type\t\tSize (bytes)\tMin Value\t\t\tMax Value");
            Console.WriteLine("--------------------------------------------------------------------------");

            DisplayTypeInfo<sbyte>("sbyte");
            DisplayTypeInfo<byte>("byte");
            DisplayTypeInfo<short>("short");
            DisplayTypeInfo<ushort>("ushort");
            DisplayTypeInfo<int>("int");
            DisplayTypeInfo<uint>("uint");
            DisplayTypeInfo<long>("long");
            DisplayTypeInfo<ulong>("ulong");
            DisplayTypeInfo<float>("float");
            DisplayTypeInfo<double>("double");
            DisplayTypeInfo<decimal>("decimal");

            Console.WriteLine("\n---------------------------------------------------------\n");

            // Part 2: Run Hacker Name Generator
            Day5.Run();
        }

        static void DisplayTypeInfo<T>(string typeName) where T : struct
        {
            Type type = typeof(T);
            int size = Marshal.SizeOf(type);

            dynamic min = GetMinValue<T>();
            dynamic max = GetMaxValue<T>();

            Console.WriteLine($"{typeName,-10}\t{size,5}\t\t{min,25}\t{max,25}");
        }

        static dynamic GetMinValue<T>() where T : struct
        {
            return typeof(T).GetField("MinValue").GetValue(null);
        }

        static dynamic GetMaxValue<T>() where T : struct
        {
            return typeof(T).GetField("MaxValue").GetValue(null);
        }
    }

    public class Day5
    {
        public static void Run()
        {
            Console.WriteLine("Welcome to the Hacker Name Generator!");

            Console.Write("What's your favorite color? ");
            string color = Console.ReadLine();

            Console.Write("What's your astrology sign? ");
            string sign = Console.ReadLine();

            Console.Write("What's your street number? ");
            string streetNumber = Console.ReadLine();

            string hackerName = $"{color}{sign}{streetNumber}";

            Console.WriteLine($"\nYour hacker name is: {hackerName}");
        }
    }
    public class CenturyConverter
    {
        public static void Run()
        {
            Console.Write("Enter number of centuries: ");
            if (!int.TryParse(Console.ReadLine(), out int centuries))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            int years = centuries * 100;
            int days = (int)(years * 365.2422); // Average year including leap years
            long hours = days * 24L;
            long minutes = hours * 60L;
            long seconds = minutes * 60L;
            long milliseconds = seconds * 1000L;
            ulong microseconds = (ulong)milliseconds * 1000;
            ulong nanoseconds = microseconds * 1000;

            Console.WriteLine($"{centuries} centuries = {years} years = {days} days = {hours} hours = " +
                              $"{minutes} minutes = {seconds} seconds = {milliseconds} milliseconds = " +
                              $"{microseconds} microseconds = {nanoseconds} nanoseconds");
        }
    }
}
