using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Messaging
{
    public interface IMessage
    {
        string[] Data { get; set; }
    }
}
