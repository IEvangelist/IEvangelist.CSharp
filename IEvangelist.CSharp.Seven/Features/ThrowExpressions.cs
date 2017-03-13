﻿using System;
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

    public class SafeGaurdIndexer<T> : List<T> where T : class
    {
        public new T this[int index]
        {
            get => base[index];
            set => base[index] = 
                value ?? throw new NullReferenceException(nameof(value));
        }
    }

    interface IService { }

    interface IContextProvider { }
}