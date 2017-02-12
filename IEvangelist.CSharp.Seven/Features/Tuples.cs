using System;
using System.Collections.Generic;
using System.Linq;

namespace IEvangelist.CSharp.Seven.Features
{
    class Tuples
    {
        // Prior to C# 7, Tuples existed as an API - but had many limitations.
        // You are probably familiar with the .Item1, .Item2, .Item3, etc...

        internal void LegacyTuple()
        {
            var letters = new Tuple<string, string>("a", "b");
            var a = letters.Item1;
            var b = letters.Item2;
        }

        internal void ValueTuple()
        {
            var letters = ("a", "b"); // ValueTuple
            var a = letters.Item1;
            var b = letters.Item2;

            // Note: ToTuple extension method
        }

        internal void NamedTuplesAssignment()
        {
            var numbers = (One: 1, Two: DateTime.Now, Three: new { Pizza = "yummy" });
            var one = numbers.One;
            var two = numbers.Two;
            var three = numbers.Three.Pizza; // Hmmm
        }

        private static (int Max, int Min) Range(IEnumerable<int> numbers)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var n in numbers)
            {
                min = (n < min) ? n : min;
                max = (n > max) ? n : max;
            }
            return (max, min);
        }

        private static (T Max, T Min) Range<T>(IEnumerable<T> enumerable)
            => (enumerable.Min(), enumerable.Max());

        private void AssignmentVsDeconstruction()
        {
            // Assignment, range has .Min and .Max
            var range = Range(new[] { 3, 5, 7, 9 });

            // Deconstruction, variables max and min are available in scope.
            (decimal max, decimal min) = Range(new[] { 3.13m, 5.7m, 7.77901m, 9.8m });

            var difference = max - min;
        }

        internal void InstantiatePerson()
        {
            var person = new Person(("David", "Pine"), 32);
            var firstName = person.Name.First;
            var lastName = person.Name.Last;
            var age = person.Age;
        }
    }

    internal class Person
    {
        internal (string First, string Last) Name { get; }

        internal int Age { get; }

        internal Person((string FirstName, string LastName) name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}