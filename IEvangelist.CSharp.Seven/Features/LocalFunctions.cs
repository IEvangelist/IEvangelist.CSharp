using System;
using System.Collections.Generic;
using System.Linq;
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

                return format(interimResult, secondResult);

                string format<TLeft, TRight>(TLeft left, TRight right)
                    => $"The results are {left} and {right}.";
            }
        }

        private Task<string> FirstWorkAsync(string address)
            => Task.FromResult("First work... done");

        private Task<string> SecondStepAsync(int index, string name)
            => Task.FromResult("Second step... done");

        internal static void LambdaVsLocal()
        {
            //              Local functions     Lambdas
            // -----------------------------------------
            // Generic      Yes                 No
            // Iterator     Yes                 No
            // Recursion    Yes                 No
            // Allocatey    No                  Yes
            // Declaration  No                  Yes

            var one = sumNumbers(1092, 7134);
            // var two = addNumbers(1092, 134);

            int sumNumbers(int x, int y) => x + y;
            Func<int, int, int> addNumbers = (x, y) => x + y;
        }

        internal static async Task DoNotEverDoThisAync()
        {
            var result =
                buildString(
                    await getAsync(0b01001000),
                    await getAsync(0b01100101),
                    await getAsync(null),
                    await getAsync(null),
                    await getAsync(0x6C),
                    await getAsync(0b01101100),
                    await getAsync(0x6F),
                    await getAsync(' '),
                    await getAsync(null),
                    await getAsync(0x57),
                    await getAsync(111),
                    await getAsync('r'),
                    await getAsync(null),
                    await getAsync(0b01101100),
                    await getAsync(100),
                    await getAsync(null));

            Console.WriteLine("I can't wait to see what the result is!");
            Console.ReadLine();
            Console.WriteLine(result);

            return; // What is this stuff below?

            ValueTask<char> getAsync(object value)
            {
                switch (value)
                {
                    case int i:
                        return new ValueTask<char>((char)i);
                    case char c:
                        return new ValueTask<char>(c);
                    case null:
                        return new ValueTask<char>('\0');

                    default:
                        throw new ArgumentException(nameof(value));
                }
            }

            string buildString(params char[] chars)
                => new string(chars.Where(c => c != '\0').ToArray());
        }
    }
}