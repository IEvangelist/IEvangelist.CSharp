using System.Collections.Generic;

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

        // This is more consistent with its usage 
        // where we use the index to assign a value
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