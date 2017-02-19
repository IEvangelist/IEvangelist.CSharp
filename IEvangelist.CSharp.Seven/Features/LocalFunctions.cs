using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEvangelist.CSharp.Seven.Features
{
    class LocalFunctions
    {
        internal static IEnumerable<char> AlphabetSubset(char start, char end)
        {
            if ((start < 'a') || (start > 'z')) throw new ArgumentOutOfRangeException(nameof(start), "start must be a letter");
            if ((end < 'a') || (end > 'z')) throw new ArgumentOutOfRangeException(nameof(end), "end must be a letter");
            if (end <= start) throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

            for (var @char = start; @char < end; ++ @char)
            {
                yield return @char;
            }
        }

        internal static IEnumerable<char> AlphabetSubset2(char start, char end)
        {
            if ((start < 'a') || (start > 'z')) throw new ArgumentOutOfRangeException(nameof(start), "start must be a letter");
            if ((end < 'a') || (end > 'z')) throw new ArgumentOutOfRangeException(nameof(end), "end must be a letter");
            if (end <= start) throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

            return AlphabetSubsetImpl(start, end);
        }

        private static IEnumerable<char> AlphabetSubsetImpl(char start, char end)
        {
            for (var @char = start; @char < end; ++ @char)
            {
                yield return @char;
            }
        }

        internal static IEnumerable<char> AlphabetSubset3(char start, char end)
        {
            if ((start < 'a') || (start > 'z')) throw new ArgumentOutOfRangeException(nameof(start), "start must be a letter");
            if ((end < 'a') || (end > 'z')) throw new ArgumentOutOfRangeException(nameof(end), "end must be a letter");
            if (end <= start) throw new ArgumentException($"{nameof(end)} must be greater than {nameof(start)}");

            return alphabetSubsetImpl();

            IEnumerable<char> alphabetSubsetImpl()
            {
                for (var @char = start; @char < end; ++ @char)
                {
                    yield return @char;
                }
            }
        }

        internal Task<string> PerformLongRunningWorkAsync(string address, int index, string name)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("An address is required", nameof(address));
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "The index must be non-negative");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("You must supply a name", nameof(name));

            return longRunningWorkImplementation();

            async Task<string> longRunningWorkImplementation()
            {
                var interimResult = await FirstWorkAsync(address);
                var secondResult = await SecondStepAsync(index, name);
                return $"The results are {interimResult} and {secondResult}.";
            }
        }

        private Task<string> FirstWorkAsync(string address)
            => Task.FromResult("First work... done");

        private Task<string> SecondStepAsync(int index, string name)
            => Task.FromResult("Second step... done");
    }
}