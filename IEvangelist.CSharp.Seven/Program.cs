using static System.Console;
using static IEvangelist.CSharp.Seven.Features.LocalFunctions;
using IEvangelist.CSharp.Seven.Features;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace IEvangelist.CSharp.Seven
{
    class Program
    {
        private static Random Random = new Random((int)DateTime.Now.Ticks);

        static void Main(string[] args)
        {
            WriteLine("C# 7 -- Demo with David Pine");

            captureIterationTimes(nums => GeneralizedAsync.SumAsync(nums).Result);
            captureIterationTimes(nums => GeneralizedAsync.SumValueAsync(nums).Result);

            void captureIterationTimes(Func<IEnumerable<int>, int> getSum)
            {
                var sw = new Stopwatch();
                sw.Start();
                for (var i = 0; i < 1000; ++ i)
                {
                    var sum = getSum(generateNumbers());
                }
                sw.Stop();
                WriteLine($"{sw.Elapsed}");
            }

            IEnumerable<int> generateNumbers()
                => Enumerable.Range(0, 1000)
                             .Select(i => i * Random.Next(1, 15));

            ReadLine();

            //GeneralizedAsync.SumAsync

            //var iterator = AlphabetSubset('f', 'a');
            //var iterator = AlphabetSubset2('f', 'a');
            //var iterator = AlphabetSubset3('f', 'a');
            var iterator = AlphabetSubset3('a', 'f');
            WriteLine("AlphabetSubset iterator created...");
            foreach (var @char in iterator) Write($"{@char}, ");
        }
    }
}