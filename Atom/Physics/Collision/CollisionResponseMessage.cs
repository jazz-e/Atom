using System;
using System.Globalization;
using Atom.Messaging;
using Atom.Physics.Collision.BoundingBox;

namespace Atom.Physics.Collision
{
    public class CollisionResponseMessage : IMessage
    {
        public string[] Data { get; set; }

        public CollisionResponseMessage(int entityId, int targetEntityId, CollisionFace face)
        {
            Data = new string[3];
            SetEntityId(entityId);
            SetCollisionFace(face);
            SetTargetEntityId(targetEntityId);
        }

        public void SetEntityId(int entityId)
        {
            Data[0] = entityId.ToString(CultureInfo.InvariantCulture);
        }

        public void SetCollisionFace(CollisionFace face)
        {
            Data[1] = face.ToString();
        }

        public void SetTargetEntityId(int target)
        {
            Data[2] = target.ToString(CultureInfo.InvariantCulture);
        }

        public int GetEntityId()
        {
            return Convert.ToInt32(Data[0]);
        }

        public CollisionFace GetCollisionFace()
        {
            CollisionFace face;
            Enum.TryParse(Data[1], out face);
            return face;
        }

        public int GetTargetEntityId()
        {
            return Convert.ToInt32(Data[2]);
        }
    }
}
