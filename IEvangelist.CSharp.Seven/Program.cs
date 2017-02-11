using static System.Console;
using static IEvangelist.CSharp.Seven.Features.LocalFunctions;

namespace IEvangelist.CSharp.Seven
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("C# 7 -- Demo with David Pine");

            //var iterator = AlphabetSubset('f', 'a');
            //var iterator = AlphabetSubset2('f', 'a');
            //var iterator = AlphabetSubset3('f', 'a');
            var iterator = AlphabetSubset3('a', 'f');
            WriteLine("AlphabetSubset iterator created...");
            foreach (var @char in iterator) Write($"{@char}, ");
        }
    }
}