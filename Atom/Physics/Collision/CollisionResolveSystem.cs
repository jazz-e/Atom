using System.Linq;
using Atom.Messaging;
using Atom.Physics.Collision.BoundingBox;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Collision
{
    public class CollisionResolveSystem : BaseSystem, IReceiver
    {

        public CollisionResolveSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof (VelocityComponent))
                .AddFilter(typeof (PositionComponent))
                .AddFilter(typeof (BoundingBoxComponent));

            PostOffice.Subscribe(this);
        }

        public int Id { get; set; }

        public void OnMessage(IMessage message)
        {
            if (message.GetType() == typeof (CollisionMessage))
            {
                CollisionMessage collisionMessage = (CollisionMessage) message;

                VelocityComponent sourceEntityVelocityComponent =
                    GetComponentsByEntityId<VelocityComponent>(collisionMessage.GetSourceCollidable()).FirstOrDefault();

                VelocityComponent targetEntityVelocityComponent = 
                    GetComponentsByEntityId<VelocityComponent>(collisionMessage.GetTargetCollidable()).FirstOrDefault();

                PositionComponent sourceEntityPositionComponent =
                   GetComponentsByEntityId<PositionComponent>(collisionMessage.GetSourceCollidable()).FirstOrDefault();

                PositionComponent targetEntityPositionComponent =
                    GetComponentsByEntityId<PositionComponent>(collisionMessage.GetTargetCollidable()).FirstOrDefault();

                BoundingBoxComponent sourceEntityBoundingBoxComponent =
                   GetComponentsByEntityId<BoundingBoxComponent>(collisionMessage.GetSourceCollidable()).FirstOrDefault();

                BoundingBoxComponent targetEntityBoundingBoxComponent =
                    GetComponentsByEntityId<BoundingBoxComponent>(collisionMessage.GetTargetCollidable()).FirstOrDefault();

                Rectangle sourceRectangle = new Rectangle(
                    (int) sourceEntityPositionComponent.X, 
                    (int) sourceEntityPositionComponent.Y,
                    sourceEntityBoundingBoxComponent.Width,
                    sourceEntityBoundingBoxComponent.Height);

                Rectangle targetRectangle = new Rectangle(
                    (int)targetEntityPositionComponent.X,
                    (int)targetEntityPositionComponent.Y,
                    targetEntityBoundingBoxComponent.Width,
                    targetEntityBoundingBoxComponent.Height);

                // Source Collided with Bottom of target
                if (sourceRectangle.Top < targetRectangle.Bottom)
                {
                    sourceEntityPositionComponent.Y = targetRectangle.Bottom;
                }

                else if (sourceRectangle.Bottom > targetRectangle.Top)
                {
                    sourceEntityPositionComponent.Y = targetRectangle.Top;
                }

                else if (sourceRectangle.Right > targetRectangle.Left)
                {
                    sourceEntityPositionComponent.X = targetRectangle.Left;
                }

                else if (sourceRectangle.Left < targetRectangle.Right)
                {
                    sourceEntityPositionComponent.X = targetRectangle.Right;
                }
            }
        }

        public TypeFilter GetMessageTypeFilter()
        {
            return new TypeFilter()
                .AddFilter(typeof (CollisionMessage));
        }
    }
}
