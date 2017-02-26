using System;

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
            private string _name;

            // Expression-bodied constructor
            public ExpressionMembersExample(string label) => Label = label;

            // Expression-bodied finalizer
            ~ExpressionMembersExample() => Console.Error.WriteLine("Finalized!");            

            // Expression-bodied get / set accessors.
            public string Label
            {
                get => _label;
                set => _label = value ?? "Unset";
            }

            // Attempting to set this to null, will throw argument null exception
            public string Name
            {
                get => _name;  // Note: throw expression
                set => _name = value ?? throw new ArgumentNullException(nameof(value), "New name must not be null");
            }
        }
    }
}