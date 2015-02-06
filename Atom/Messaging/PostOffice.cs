using System;
using System.Collections.Generic;

namespace Atom.Messaging
{
    public class PostOffice
    {
        private static PostOffice _instance = new PostOffice();

        private HashSet<IReceiver> _receivers = new HashSet<IReceiver>();

        private PostOffice()
        {
            
        }

        /// <summary>
        /// Subscribes the receiver to the PostOffice.
        /// </summary>
        /// <param name="receiver">The receiver to subscribe</param>
        public static void Subscribe(IReceiver receiver)
        {
            GetInstance()._receivers.Add(receiver);
        }

        private static PostOffice GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// Sends a message to all the receivers that are subscribed to the message type.
        /// </summary>
        /// <param name="message">The message you want to send</param>
        /// <returns>Returns TRUE or FALSE to whether the message has been sent to at least 1 receiver</returns>
        public static bool SendMessage(IMessage message)
        {
            bool hasSent = false;

            foreach (IReceiver receiver in GetInstance()._receivers)
            {
                if (receiver.GetMessageTypeFilter().Contains(message.GetType()))
                {
                    receiver.OnMessage(message);
                    hasSent = true;
                }
            }

            return hasSent;
        }
    }
}
