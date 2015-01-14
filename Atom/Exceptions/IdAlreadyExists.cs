using System;

namespace Atom.Exceptions
{
    public class IdAlreadyExists : Exception
    {
        public override string Message
        {
            get
            {
                return "The given id already exists in collection";
            }
        }
    }
}
