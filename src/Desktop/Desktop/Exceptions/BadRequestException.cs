using System;

namespace Desktop.Exceptions
{
    class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
