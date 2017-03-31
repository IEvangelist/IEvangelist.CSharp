using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
    }
}