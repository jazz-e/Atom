using System;

namespace Atom.Exceptions
{
    public class NotILoggerException : Exception
    {
        public override string Message
        {
            get
            {
                return "The given object/type coudn't be derived from ILogger";
            }
        }
    }
}
