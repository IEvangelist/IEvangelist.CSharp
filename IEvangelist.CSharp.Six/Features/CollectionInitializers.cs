using System;
using System.Collections.Generic;
using System.Text;

namespace IEvangelist.CSharp.Six.Features
{
    class CollectionInitializers
    {
        internal IDictionary<string, int> LegacyStringMap = new Dictionary<string, int>
        {
            { "Zero", 0 },
            { "One",  1 },
            { "Two", 2 },
            { "Three", 3 }
        };

        internal IDictionary<string, int> StringMap = new Dictionary<string, int>
        {
            ["Zero"] = 0,
            ["One"] = 1,
            ["Two"] = 2,
            ["Three"] = 3
        };

        internal IDictionary<int, string> IntMap = new Dictionary<int, string>
        {
            [0] = "Zero",
            [1] = "One",
            [2] = "Two"
        };
    }
}