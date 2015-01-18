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

        public override void Update(GameTime gameTime, int entityId)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, int entityId)
        {
            
        }

        public override void Purge<T>(int entityId)
        {
            
        }

        public override void Disable<T>(int entityId)
        {
            
        }

        public override void Enable<T>(int entityId)
        {
            
        }

        public override void RemoveEntityComponents(int entityId)
        {
            PositionComponents.RemoveAll(component => component.EntityId == entityId);
            VelocityComponents.RemoveAll(component => component.EntityId == entityId);
        }

        public override void AddComponent(Component component)
        {
            if (component.GetType() == typeof (PositionComponent))
            {
                PositionComponents.Add((PositionComponent)component);
            }
            else if (component.GetType() != typeof (VelocityComponent))
            {
                VelocityComponents.Add((VelocityComponent)component);                
            }


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
