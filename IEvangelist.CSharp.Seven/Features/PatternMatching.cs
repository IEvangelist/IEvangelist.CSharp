using System;
using System.Collections.Generic;
using static System.Console;
using static System.Int32;
using static System.Math;

namespace IEvangelist.CSharp.Seven.Features
{
    class PatternMatching
    {
        // C# 7.0 introduces the notion of patterns, which, abstractly speaking, are syntactic elements 
        // that can test that a value has a certain "shape", and extract information from the value when it does.

        // Constant and type patterns
        // Is Expressions

        internal void PrintStars(object obj)
        {
            if (obj is null)
            {
                return; // Constant pattern "null"
            }
            if (!(obj is int i))
            {
                return; // Type pattern "int i"
            }

            WriteLine(new string('*', i));

            var f = i; // Note: that "i" is still available to us in this scope.
        }

        internal static void WriteInt32(object o)
        {
            if (o is int i || (o is string s && TryParse(s, out i)))
            {
                WriteLine(i);
            }
            else
            {
                WriteLine($"{o} is not an int");
            }
        }

        // The switch statement is being generalized so that:

        // You can switch on any type (not just primitive types)
        // Patterns can be used in case clauses
        // Case clauses can have additional conditions on them

        internal class Shape
        {
            protected internal double Height { get; }

            protected internal double Length { get; }

            protected Shape(double height = 0, double length = 0)
            {
                Height = height;
                Length = length;
            }
        }

        class Circle : Shape
        {
            internal double Radius => (Height / 2) + (Pow(Length, 2) / 8 * Height);

            internal Circle(double height = 0, double length = 0) : base(height, length) { }
        }

        class Rectangle : Shape
        {
            internal bool IsSquare => Height == Length;

            internal Rectangle(double height = 0, double length = 0) : base(height, length) { }
        }

        internal static void OutputShapes(IEnumerable<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                switch (shape)
                {
                    case Circle c:
                        WriteLine($"circle with radius {c.Radius}");
                        break;
                    case Rectangle s when (s.IsSquare):
                        WriteLine($"{s.Length} x {s.Height} square");
                        break;
                    case Rectangle r:
                        WriteLine($"{r.Length} x {r.Height} rectangle");
                        break;
                    default:
                        WriteLine("This is not a shape that we're familiar with...");
                        break;
                    case null:
                        throw new ArgumentNullException(nameof(shape));
                }
            }
        }
    }
}