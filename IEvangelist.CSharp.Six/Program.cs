using System;
using IEvangelist.CSharp.Six.Features;
using static System.Console;
using static System.DateTime;

namespace IEvangelist.CSharp.Six
{
    class Program
    {
        private void ThrowHelper(Version version)
        {
            if (version == null)
            {
                //throw new ArgumentNullException("version"); // Before C# 6
                throw new ArgumentNullException(nameof(version));
            }
        }

        //private const string David = "David";
        private const string David = nameof(David);
        private const string Pine = nameof(Pine);        

        private static readonly string FullName = $"{David} {Pine}";

        private static DateTime DateOfBirth = new DateTime(1984, 7, 7);

        private static double AgeInDays => (Now - DateOfBirth).TotalDays;

        /// <summary>Perfect for the singleton pattern, only set once.</summary>
        private static string PropertyFullName { get; } = FullName;

        /// <summary>Re-evaluates the expression with every access.</summary>
        private static string Greeting 
            => $"{FullName}... who just so happens to be {AgeInDays:#,#} " + // don't do this...
               $"days old and was born on {DateOfBirth:MMMM d yyyy}";

        static void Main(string[] args)
        {
            WriteLine($"C# 6 -- Demo with {Greeting}");
            WriteLine();

            var example = new AwaitErrorHandling();
            example.RequestStatusChanged += status => WriteLine(status);
            do
            {
                WriteLine(example.GetJokeAsync().Result);
                WriteLine("\nType 'Y' for another joke.\n");
            } while (ReadKey().Key == ConsoleKey.Y);
            
            ReadLine();
        }
    }
}

namespace Interesting
{
    public class CustomConsole
    {
        // When static, this is ambiguous
        public void WriteLine(string message)
        {
            // Obviously...
        }
    }
}