using System;
using System.Collections.Generic;

namespace Atom.Messaging
{
    public class PostOffice
    {
        private static PostOffice _instance = new PostOffice();

        private HashSet<IReceiver> _receivers = new HashSet<IReceiver>();

        private int _currentReceiverId = 0;

        private PostOffice()
        {
            
        }

        public static void Subscribe(IReceiver receiver)
        {
            receiver.Id = GetInstance().GetNextId();

            GetInstance()._receivers.Add(receiver);
        }

        private static PostOffice GetInstance()
        {
            return _instance;
        }

        public static bool SendMessage(IMessage message)
        {
            bool hasSent = false;

            foreach (IReceiver receiver in GetInstance()._receivers)
            {
                if (receiver.GetTypeFilter().Contains(message.GetType()))
                {
                    receiver.OnMessage(message);
                    hasSent = true;
                }
            }

            return hasSent;
        }

        private int GetNextId()
        {
            return ++_currentReceiverId;
        }


    }
}
