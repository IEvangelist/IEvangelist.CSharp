﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IEvangelist.CSharp.Seven.Features
{
    class Tuples
    {
        // Prior to C# 7, Tuples existed as an API - but had many limitations.
        // You are probably familiar with the .Item1, .Item2, .Item3, etc...

        // Benefits:
        //    Readability and "Immutable", relies on System.ValueTuple.dll
        
        static void LegacyTuple()
        {
            var letters = new Tuple<string, string>("a", "b");
            var a = letters.Item1;
            var b = letters.Item2;

            // letters.Item1 = "c";
        }

        static void ValueTuple()
        {
            var letters = ('a', 'b'); // ValueTuple
            var a = letters.Item1;
            var b = letters.Item2;

            // OMG, WTF?!
            letters.Item1 = 'c';

            // Note: ToTuple extension method
            var systemTuple = letters.ToTuple();
            var c = systemTuple.Item1;
            var d = systemTuple.Item2;

            var stringAndNumber = ("programming is fun", 7);
            var s = stringAndNumber.Item1;
            var n = stringAndNumber.Item2;

            // Example to show IntelliSense
            var wordAndNum = (Word: "programming", Number: 7);

            // Item1 and Item2 are actually hidden
            var word = wordAndNum.Word; 
            var number = wordAndNum.Number;

            // OMG, WTF?!
            wordAndNum = ("is fun", 10);
            
            // Deconstruction
            (string str0, int num0) = wordAndNum;
            (var str1, var num1) = wordAndNum;
            var (str2, num2) = wordAndNum;
            
            if (s == str0 && n == num0 &&
                s == str1 && n == num1 &&
                s == str2 && n == num2)
            {
                // This would get executed...
            }

            // More examples
            var (now, tenSeconds) =
                (DateTime.Now, TimeSpan.FromSeconds(10));
        }

        static void NamedTuplesAssignment()
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

        private static (T Min, T Max) Range<T>(IEnumerable<T> enumerable)
            => (enumerable.Min(), enumerable.Max());

        static void AssignmentVsDeconstruction()
        {
            // Assignment, range has .Min and .Max
            var range = Range(new[] { 3, 5, 7, 9 });

            // Deconstruction, variables max and min are available in scope.
            var (min, max) =
                Range<decimal>(new[] { 3.13m, 5.7m, 7.77901m, 9.8m });
            
            var difference = max - min;
        }

        static void InstantiatePerson()
        {
            var person = new Person(("David", "Pine"), 32);
            var firstName = person.Name.First;
            var lastName = person.Name.Last;
            var age = person.Age;

            // Using explicitly defined "Deconstruct"
            // method on a non-tuple object
            var (_, _, _) = person;
            var (first, _, _) = person;
            var (frst, lst, _) = person;
            var (f, l, a) = person;

            // Cherry pick
            var (_, theLastName, _) = person;
            
            // Note: the use of the _ doesn't actually declare the variable
            // It is not available, it is a way to ignore that ordinal.
        }

        static void Main()
        {
            ValueTuple();

            var davidPine = new Person(("David", "Pine"), 32);
            var someoneElse = new Person(("Someone", "Else"), -1);

            davidPine.CopyTo(someoneElse);

            Console.WriteLine($"{nameof(someoneElse)} is now {someoneElse}");
            Console.WriteLine(davidPine.Name);
        }
    }

    internal class Person
    {
        internal (string First, string Last) Name { get; private set; }

        internal int Age { get; private set; }

        internal Person((string FirstName, string LastName) name, 
                        int age)
        {
            Name = name;
            Age = age;
        }

        public void Deconstruct(out (string, string) name,
                                out int age)
        {
            name = Name;
            age = Age;
        }

        public void Deconstruct(out string first, 
                                out string last, 
                                out int age)
        {
            first = Name.First;
            last = Name.Last;
            age = Age;
        }

        public void CopyTo(Person person) 
            => (person.Name, person.Age) = (Name, Age);

        public override string ToString() 
            => $"{Name.First} {Name.Last} ({Age})";
    }
}