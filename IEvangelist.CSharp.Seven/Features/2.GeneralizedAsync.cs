using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;


namespace IEvangelist.CSharp.Seven.Features
{
    // Returning a Task object from async methods can introduce performance 
    // bottlenecks in certain paths. Task is a reference type, so using it 
    // means allocating an object. In cases where a method declared with 
    // the async modifier returns a cached result, or completes synchronously,
    // the extra allocations can become a significant time cost in performance 
    // critical sections of code.

    class GeneralizedAsync
    {
        internal static async Task<int> SumAsync(IEnumerable<int> numbers)
        {
            if (numbers.Any())
            {
                return await Task.Run(() => numbers.Sum());
            }

            return await Task.FromResult(0);
        }

        internal static ValueTask<int> SumValueAsync(IEnumerable<int> numbers)
        {
            if (numbers.Any())
            {
                return new ValueTask<int>(Task.Run(() => numbers.Sum()));
            }

            return new ValueTask<int>(0);
        }

        public ValueTask<long> GetDirectorySizeAsync(
            string path,
            string searchPattern)
        {
            if (!Directory.EnumerateFileSystemEntries(path, searchPattern)
                          .Any())
            {
                return new ValueTask<long>(0);
            }
            else
            {
                return new ValueTask<long>(
                    Task.Run(() =>
                    Directory.GetFiles(path,
                                       searchPattern,
                                       SearchOption.AllDirectories)
                             .Sum(t => new FileInfo(t).Length)));
            }
        }

        private static Random Random = new Random((int)DateTime.Now.Ticks);

        static void Main()
        {
            var numbers = generateNumbers().ToList();
            captureIterationTimes(nameof(Task<int>),
                nums => GeneralizedAsync.SumAsync(nums).Result, numbers);
            captureIterationTimes(nameof(ValueTask<int>),
                nums => GeneralizedAsync.SumValueAsync(nums).Result, numbers);

            void captureIterationTimes(string type, Func<IEnumerable<int>, int> getSum, List<int> ints)
            {
                var sw = new Stopwatch();
                sw.Start();

                5000.Times(() => getSum(ints));

                sw.Stop();
                Console.WriteLine($"{type.PadLeft(10)} {sw.Elapsed}");
            }

            IEnumerable<int> generateNumbers()
                => Enumerable.Range(0, 1500)
                             .Select(i => i * Random.Next(1, 15));

        }
    }

    static class IntExtensions
    {
        internal static void Times(this int count, Action action)
        {
            for (int i = 0; i < count; ++ i)
            {
                action();
            }
        }
    }
}