using System;
using System.Collections.Generic;
using Atom.Messaging;
using Atom.Physics;
using Microsoft.Xna.Framework;
using Atom;
using Atom.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace AtomDemo
{
    public class TestSystem : Atom.BaseSystem, IReceiver
    {
        public int Id { get; set; }
        public List<PositionComponent> PositionComponents;  
        public List<VelocityComponent> VelocityComponents;  

        public TestSystem()
        {
            PostOffice.Subscribe(this);
        }

        public void SendMessage()
        {
            PostOffice.SendMessage(new NewMessage());
        }

        public TypeFilter GetMessageTypeFilter()
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
