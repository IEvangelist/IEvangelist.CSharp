using System;
using System.Collections.Generic;
using System.Linq;

namespace IEvangelist.CSharp.Six.Features
{
    class NullConditionalOperator
    {
        List<string> _messages { get; } = new List<string>();

        internal event Action<string> MessageCreated;

        internal void OnMessageCreated(string message)
        {
            var handler = MessageCreated;
            if (handler == null)
            {
                return;
            }
            handler(message);
        }

        internal void OnMessageCreatedNew(string message)
            => MessageCreated?.Invoke(message);

        internal bool NullableBoolReturns()
        {
            if (_messages?.Any() == true)
            {
                // Wait a sec... I thought the .Any() returns bool.
                // Indeed it does, but with null propogation it could be null
                // So the entire expression is actually a bool?
            }

            if ((_messages == null ? (bool?)null : _messages.Any()) == true)
            {
            }

            if ((_messages?.Any()).GetValueOrDefault())
            {
            }

            if (_messages == null ? false : _messages.Any())
            {
            }

            throw null; // Wow, this is lame...
        }
    }
}