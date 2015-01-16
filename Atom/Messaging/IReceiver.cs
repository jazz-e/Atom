using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Messaging
{
    public interface IReceiver
    {
        int Id { get; set; }

        void OnMessage(IMessage message);

        TypeFilter GetTypeFilter();
    }
}
