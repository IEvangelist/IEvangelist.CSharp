using System;
using System.Collections.Generic;

namespace IEvangelist.CSharp.Seven.Features
{
    // In C#, throw has always been a statement.
    // Because throw is a statement, not an expression, 
    // there were C# constructs where you could not use it. 
    // These included conditional expressions, null 
    // coalescing expressions, and some lambda expressions.
    // The addition of expression-bodied members adds more 
    // locations where throw expressions would be useful. 
    
    class LegacyService : IService
    {
        private readonly IContextProvider _povider;

        public LegacyService(IContextProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            _povider = provider;
        }
    }

    class ModernService : IService
    {
        private readonly IContextProvider _povider;

        public ModernService(IContextProvider provider)
        {
            _povider = 
                provider ?? throw new ArgumentNullException(nameof(provider));
        }
    }

    public class SafeSetList<T> : List<T> where T : class
    {
        // Don't actually do this, the "new" keyword hides

        public new T this[int index]
        {
            get => base[index];
            set => base.Add(
                value ?? throw new NullReferenceException(nameof(value)));
        }
    }

    static class ThrowExpressions
    {
        static void Main()
        {
            var list = new SafeSetList<Version>();
            list[0] = new Version(1, 0, 0, 0);
            try
            {
                list[1] = null; // Goes bang!
            }
            catch
            {
                Console.WriteLine("Safety first!");
            }
        }
    }

    interface IService { }

    interface IContextProvider { }
}