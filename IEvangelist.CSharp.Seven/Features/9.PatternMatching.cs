using System.Collections.Generic;
using static System.Console;
using static System.Int32;
using static System.Math;

namespace IEvangelist.CSharp.Seven.Features
{
    class PatternMatching
    {
        // C# 7.0 introduces the notion of patterns, which, abstractly speaking, 
        // are syntactic elements that can test that a value has a certain "shape", 
        // and extract information from the value when it does.

        // Constant and type patterns
        // Is Expressions

        static void PrintStars(object obj)
        {
            if (obj is null) // Constant pattern "null", instead of (obj == null)
            {
                return;
            }

            // Look closely at this syntax. This is where we start mixing
            // metaphors. Historically in C# we could use "is" to do simply 
            // type assertions "obj is [type]", and we all know how to declare
            // a variable "int i". This new syntax merges these concepts 
            // together and is more compound / expressive.

            // (obj is int i)
            // "obj is int"     // type assertion
            //        "int i"   // declaration

            if (!(obj is int i)) // Type pattern "int i"
            {
                return;
            }

            WriteLine(new string('*', i));

            var f = i; // Note: that "i" is still available to us in this scope.
        }

        static void WriteInt32(object obj)
        {
            // Notice that "i" has a declaration from the "is expression"
            // We can re-use that declaration in our "TryParse".
            // If the order of these two checks are swapped, we'll get a
            // compilation error as "i" is not declared.

            if (obj is int i || (obj is string s && TryParse(s, out i)))
            {
                WriteLine(i);
            }
            else
            {
                WriteLine($"{obj} is not an int [ System.Int32 ]");
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

        internal class Circle : Shape
        {
            internal double Radius => Height / 2;

            internal double Diameter => Radius * 2;

            internal double Circumference => 2 * PI * Radius;

            internal Circle(double height = 10, double length = 10) 
                : base(height, length) { }
        }

        internal class Rectangle : Shape
        {
            internal bool IsSquare => Height == Length;

            internal Rectangle(double height = 10, double length = 10) 
                : base(height, length) { }
        }

        static void OutputShapes(IEnumerable<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                // Previously, this was not permitted. Types had to be concrete
                // such as enums, numerics, bools, strings, etc.
                switch (shape)
                {
                    case Circle c:
                        WriteLine($"circle with circumference {c.Circumference}");
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
                    //case null:
                    //    throw new ArgumentNullException(nameof(shape));
                }
            }
        }

        static void Main()
        {
            OutputShapes(
                new List<Shape>
                {
                    new Circle(7, 7),
                    new Rectangle(20, 9),
                    null,
                    new Rectangle(32, 32)
                });
        }
    }
}