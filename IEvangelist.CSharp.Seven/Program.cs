using static System.Console;
using static IEvangelist.CSharp.Seven.Features.LocalFunctions;
using IEvangelist.CSharp.Seven.Features;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace IEvangelist.CSharp.Seven
{
    class Program
    {
        private static Random Random = new Random((int)DateTime.Now.Ticks);

        static void Main(string[] args)
        {
            WriteLine("C# 7 -- Demo with David Pine");

            captureIterationTimes(nameof(Task<int>), 
                nums => GeneralizedAsync.SumAsync(nums).Result);
            captureIterationTimes(nameof(ValueTask<int>), 
                nums => GeneralizedAsync.SumValueAsync(nums).Result);

            void captureIterationTimes(string type, Func<IEnumerable<int>, int> getSum)
            {
                var sw = new Stopwatch();
                sw.Start();
                for (var i = 0; i < 5000; ++ i)
                {
                    var sum = getSum(generateNumbers());
                }
                sw.Stop();
                WriteLine($"{type.PadLeft(10)} {sw.Elapsed}");
            }

            IEnumerable<int> generateNumbers()
                => Enumerable.Range(0, 1500)
                             .Select(i => i * Random.Next(1, 15));

            waitForEnterKey();
            
            //var iterator = AlphabetSubset('f', 'a');
            //var iterator = AlphabetSubset2('f', 'a');
            //var iterator = AlphabetSubset3('f', 'a');
            var iterator = AlphabetSubset3('a', 'f');
            WriteLine("AlphabetSubset iterator created...");
            foreach (var @char in iterator)
            {
                Write($"{@char}, ");
            }
            waitForEnterKey();

            DoNotEverDoThisAync().Wait();

            waitForEnterKey();

            RefLocalsAndReturns.ExampleOne();
            RefLocalsAndReturns.ExampleTwo();

            waitForEnterKey();

            var example = string.Join("", new[] { null, "", "7" }.Select(str => str.ToInt32()));
            WriteLine($"{example} Bond, James Bond (shaken, not stirred)...");

            void waitForEnterKey()
            {
                WriteLine();
                WriteLine("Press [Enter] key to continue...");
                ReadLine();
                WriteLine();
            }
        }
    }
}