using System;

namespace Utilities
{
    public class CustomException : Exception
    {
        public CustomException(string message, Exception inner = null)
            : base($"[AUTOMATION ERROR] {message}", inner) { }
    }
}