using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atom.Messaging;

namespace Atom.Physics.Movement
{
    public class MoveMessage : IMessage
    {
        public string[] Data { get; set; }

        public MoveMessage()
        {
            Data = new string[2];
        }

        public MoveMessage(int entityId, MoveDirection direction) : this()
        {
            SetEntityId(entityId);
            SetMoveDirection(direction);
        }

        public MoveMessage SetMoveDirection(MoveDirection direction)
        {
            Data[0] = direction.ToString();

            return this;
        }

        public MoveDirection GetMoveDirection()
        {
            MoveDirection moveDirection = MoveDirection.None;
            if (Data[0] != null)
            {
                MoveDirection.TryParse(Data[0], out moveDirection);
            }

            return moveDirection;
        }

        public MoveMessage SetEntityId(int entityId)
        {
            Data[1] = entityId.ToString();

            return this;
        }

        public int GetEntityId()
        {
            int id = 0;

            if (Data[1] != null)
            {
                id = Convert.ToInt32(Data[1]);
            }

            return id;
        }
    }
}
