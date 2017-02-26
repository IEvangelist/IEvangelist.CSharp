using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEvangelist.CSharp.Seven.Features
{
    class GeneralizedAsync
    {
        internal static Task<int> SumAsync(IEnumerable<int> numbers)
        {
            if (numbers.Any())
            {
                return Task.Run(() => numbers.Sum());
            }

            return Task.FromResult(0);
        }

        internal static ValueTask<int> SumValueAsync(IEnumerable<int> numbers)
        {
            if (numbers.Any())
            {
                return new ValueTask<int>(Task.Run(() => numbers.Sum()));
            }

            return new ValueTask<int>(0);
        }
    }
}