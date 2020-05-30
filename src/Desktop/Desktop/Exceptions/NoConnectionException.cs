using System;

namespace Desktop.Exceptions
{
    class NoConnectionException : Exception
    {
        public NoConnectionException(string message) : base(message) { }
    }
}
