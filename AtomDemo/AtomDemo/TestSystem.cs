using System;
using System.Collections.Generic;
using Atom.Messaging;
using Microsoft.Xna.Framework;
using Atom;
using Atom.Graphics;

namespace AtomDemo
{
    public class TestSystem : Atom.System, IReceiver
    {
        public int Id { get; set; }

        public TestSystem()
        {
            PostOffice.Subscribe(this);
        }

        public void SendMessage()
        {
            PostOffice.SendMessage(new NewMessage());
        }

        public TypeFilter GetTypeFilter()
        {
            return new TypeFilter()
                .AddFilter(typeof(NewMessage));
        }

        public void OnMessage(IMessage message)
        {
            Console.WriteLine(message.Data[0]);
        }
    }

    public class NewMessage : IMessage
    {
        public string[] Data 
        {
            get
            {
                return new[] { "Message Transferred!" };
            }
            set
            {

            } 
        }
    }
}
