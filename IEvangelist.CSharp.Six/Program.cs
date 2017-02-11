using IEvangelist.CSharp.Six.Features;
using System;
using static System.Console;
using static System.DateTime;

namespace IEvangelist.CSharp.Six
{
    class Program
    {
        /// <summary>Using the nameof operator is extremely powerful</summary>
        private const string David = nameof(David);
        private const string Pine = nameof(Pine);

        /// <summary>String interpolation</summary>
        private static readonly string FullName = $"{David} {Pine}";

        private static DateTime DateOfBirth = new DateTime(1984, 7, 7);
        private static double AgeInDays = (Now - DateOfBirth).TotalDays;

        /// <summary>Perect for the singleton pattern, only set once.</summary>
        private static string PropertyFullName { get; } = FullName;

        /// <summary>Re-evaluates the expression with every access.</summary>
        private static string Greeting 
            => $"{FullName}... who just so happens to be {AgeInDays:#,#} days old!";

        static void Main(string[] args)
        {
            WriteLine($"C# 6 -- Demo with {Greeting}");
            WriteLine();

            var example = new AwaitErrorHandling();
            example.RequestStatusChanged += status => WriteLine(status);
            WriteLine(example.GetJokeAsync().Result);
            WriteLine();
        }
    }
}