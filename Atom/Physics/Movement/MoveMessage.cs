using System;
using Atom.Messaging;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Movement
{
    public class MoveMessage : IMessage
    {
        public string[] Data { get; set; }

        public MoveMessage()
        {
            Data = new string[3];
        }

        public MoveMessage(int entityId, Vector2 force) : this()
        {
            SetEntityId(entityId);
            SetForce(force);
        }

        public MoveMessage SetEntityId(int entityId)
        {
            Data[0] = entityId.ToString();

            return this;
        }

        public int GetEntityId()
        {
            int id = 0;

            if (Data[0] != null)
            {
                id = Convert.ToInt32(Data[0]);
            }

            return id;
        }

        public MoveMessage SetForce(Vector2 force)
        {
            Data[1] = Convert.ToString(force.X);
            Data[2] = Convert.ToString(force.Y);

            return this;
        }

        public Vector2 GetForce()
        {
            return new Vector2((float) Convert.ToDouble(Data[1]), (float) Convert.ToDouble(Data[2]));
        }
    }
}
