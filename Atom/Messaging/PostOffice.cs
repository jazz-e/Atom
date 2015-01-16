using System;
using System.Collections.Generic;

namespace Atom.Messaging
{
    public class PostOffice
    {
        private static PostOffice _instance = new PostOffice();

        private Dictionary<string, Inbox> _inboxes = new Dictionary<string, Inbox>();

        private PostOffice()
        {
            
        }

        public void Subscribe(List<Type> types)
        {
            
        }

    }
}
