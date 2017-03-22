using System;
using System.Collections.Generic;

namespace IEvangelist.CSharp.Seven.Features
{
    class MoreExpressionBodiedMembers
    {
        // C# 6 introduced expression-bodied members for member functions, 
        // and read-only properties.
        //
        // C# 7 expands this and you can implement them on:
        //     constructors
        //     finalizers
        //     get and set accessors on properties and indexers

        private class ExpressionMembersExample
        {
            private string _label;
            private IDictionary<int, string> _example =
                new Dictionary<int, string>
                {
                    [0] = "Zero",
                    [1] = "One",
                    [2] = "Two"
                };

            // Expression-bodied constructor
            public ExpressionMembersExample(string label) 
                => Label = label;

            // Expression-bodied finalizer
            ~ExpressionMembersExample() 
                => Console.Error.WriteLine("Finalized!");            

            // Expression-bodied get / set accessors.
            public string Label
            {
                get => _label;
                set => _label = value ?? "Unset";
            }

            public string this[int index]
            {
                get => _example.TryGetValue(index, out var result) ? result : "";
                set => _example[index] = value;
            }
        }
    }
}