using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Messaging
{
    public interface IReceiver
    {
        /// <summary>
        /// Called when the PostOffice that receives a message that matches the filter
        /// </summary>
        /// <param name="message">The message that was received to the PostOffice</param>
        void OnMessage(IMessage message);

        /// <summary>
        /// Gets the TypeFilter for the receiver
        /// </summary>
        /// <returns>Returns the TypeFilter for the receiver</returns>
        TypeFilter GetMessageTypeFilter();
    }
}
