using System;

namespace IEvangelist.CSharp.Seven.Features
{
    class RefLocalsAndReturns
    {
        #region Matrix
        
        static int[,] matrix = {
            { 16, 900, 217, 829, 73, 2, 7, 63, 22 },
            { 51, 31, 107, 895, 19, 52, 3, 6, 11 },
            { -1, 44, -17, 88, 398, 2, 53, 36, 63 },
            { 1, 9, 17, 89, 99, 2, 3, 6, 92 },
            { 100, -9, 217, 89, -11, 2, 36, 111, 23 },
            { 31, 0, 17, -8, 19, 42, 333, 6, 192 },
            { 1, 16, 1, -89, 9, -1, 33, 6, -92 },
            { 21, 66, 7, 321, 91, 2, 3, 8, 92 }
        };

        #endregion

        // For public API, class or struct is preferred...

        static (int i, int j) Find(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); ++ i)
                for (int j = 0; j < matrix.GetLength(1); ++ j)
                    if (predicate(matrix[i, j]))
                        return (i, j);

            return (-1, -1); // Not found
        }

        static ref int FindReference(int[,] matrix, Func<int, bool> predicate)
        {
            for (int i = 0; i < matrix.GetLength(0); ++ i)
                for (int j = 0; j < matrix.GetLength(1); ++ j)
                    if (predicate(matrix[i, j]))
                        return ref matrix[i, j];

            throw new InvalidOperationException("Not found");
        }

        static void Main()
        {
            int serach = 42,
                replacement = 24,
                refAssignment = 777;

            var indices = Find(matrix, (val) => val == serach);
            Console.WriteLine(indices);
            matrix[indices.i, indices.j] = replacement;

            ref var item = ref FindReference(matrix, (val) => val == replacement);
            Console.WriteLine(item);
            item = refAssignment; // This is a reference
            Console.WriteLine(matrix[5, 5]);
        }
    }
}