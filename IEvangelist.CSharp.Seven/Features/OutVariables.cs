﻿using System;
using System.Collections.Generic;
using System.Net;

namespace IEvangelist.CSharp.Seven.Features
{
    class OutVariables
    {
        internal int LegacyTryPattern(string number)
        {
#pragma warning disable IDE0018 // Inline variable declaration
            int value;
#pragma warning restore IDE0018 // Inline variable declaration
            if (int.TryParse(number, out value))
            {
                return value;
            }
            else
            {
                return -1;
            }

            // The value variable is valid here
        }

        internal int NewTryPattern(string number)
        {
            if (int.TryParse(number, out var value)) // Note: we can be explicit, but implicit is valid
            {
                return value;
            }
            else
            {
                return -1; // value is available to us here.
            }

            // the value variable is not valid outside the if block scope.
        }

        internal int ExpressiveTryPattern(string number)
            => int.TryParse(number, out int value) ? value : -1;
    }

    static class StringExtensions
    {
        delegate bool TryParseDelegate<T>(string s, out T result);

        static T To<T>(string value, TryParseDelegate<T> parse)
            => parse(value as string, out T result) ? result : default(T);

        internal static int ToInt32(this string value) => To<int>(value, int.TryParse);

        internal static DateTime ToDateTime(this string value) => To<DateTime>(value, DateTime.TryParse);

        internal static IPAddress ToIPAddress(this string value) => To<IPAddress>(value, IPAddress.TryParse);

        internal static TimeSpan ToTimeSpan(this string value) => To<TimeSpan>(value, TimeSpan.TryParse);
    }
}