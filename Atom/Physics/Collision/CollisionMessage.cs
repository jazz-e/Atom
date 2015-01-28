using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atom.Messaging;

namespace Atom.Physics.Collision
{
    public class CollisionMessage : IMessage
    {
        public string[] Data { get; set; }

        public CollisionMessage(int sourceEntityid, int targetEntityId)
        {
            Data = new string[3];

            SetSourceCollidable(sourceEntityid);
            SetTargetCollidable(targetEntityId);
        }
        
        public void SetSourceCollidable(int entityId)
        {
            Data[0] = entityId.ToString();
        }

        public int GetSourceCollidable()
        {
            return Convert.ToInt32(Data[0]);
        }

        public void SetTargetCollidable(int entityId)
        {
            Data[1] = entityId.ToString();
        }

        public int GetTargetCollidable()
        {
            return Convert.ToInt32(Data[1]);
        }

        public void SetElaspedTime(int )
    }
}
