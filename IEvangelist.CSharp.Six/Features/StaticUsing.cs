using static System.Console;
using static System.String; // NOTE: doesn't work with string keyword
using static System.Math;

namespace IEvangelist.CSharp.Six.Features
{
    class StaticUsing
    {
        // NOTE: This doesn't work for extension methods.

        internal void Output(string message)
            => WriteLine(IsNullOrWhiteSpace(message)
                ? $"The given {nameof(message)} is unknown"
                : message);

        internal void WriteSqrt(double area) 
            => WriteLine(Sqrt(area));
    }
}