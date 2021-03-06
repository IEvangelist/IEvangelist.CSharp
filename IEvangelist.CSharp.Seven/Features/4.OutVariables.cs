﻿using System;
using System.Net;
using System.Linq;

namespace IEvangelist.CSharp.Seven.Features
{
    class OutVariables
    {
        internal int LegacyTryPattern(string number)
        {
#pragma warning disable IDE0018 // Inline variable declaration
            int i;
#pragma warning restore IDE0018 // Inline variable declaration
            if (int.TryParse(number, out i))
            {
                return i;
            }
            else
            {
                return -1;
            }

            // The "i" variable is valid here
        }

        internal int NewTryPattern(string number)
        {
            // Note: we can be explicit, but implicit is valid
            if (int.TryParse(number, out int i))
            {
                return i;
            }
            else
            {
                return -1; // "i" is available to us here.
            }
            // The "i" variables leaks into this scope.
        }

        internal void ScopeExample()
        {
            if (DateTime.TryParse("OMG", out var date))
            {
                if (TimeSpan.TryParse("What's happening?", out var ts))
                {
                    var newDate = date.Add(ts);
                }
            }

            // Note: the "ts" variable is NOT leaked in this scope!
        }

        internal int ExpressiveTryPattern(string number)
            => int.TryParse(number, out int value) ? value : -1;

        static void Main()
        {
            var example = string.Join("", new[] { null, "", "7" }.Select(str => str.ToInt32()));
            Console.WriteLine($"{example} Bond, James Bond (shaken, not stirred)...");
        }
    }

    static class StringExtensions
    {
        private delegate bool TryParseDelegate<T>(string s, out T result);

        private static T To<T>(string value, TryParseDelegate<T> parse)
            => parse(value, out T result) ? result : default(T);

        internal static int ToInt32(this string value) 
            => To<int>(value, int.TryParse);

        internal static DateTime ToDateTime(this string value) 
            => To<DateTime>(value, DateTime.TryParse);

        internal static IPAddress ToIPAddress(this string value) 
            => To<IPAddress>(value, IPAddress.TryParse);

        internal static TimeSpan ToTimeSpan(this string value) 
            => To<TimeSpan>(value, TimeSpan.TryParse);
    }
}