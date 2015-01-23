using System;
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


                bool travelingUp = Math.Sign(sourceEntityVelocityComponent.Velocity.Y) == -1;
                bool travelingDown = Math.Sign(sourceEntityVelocityComponent.Velocity.Y) == 1;
                bool travelingRight = Math.Sign(sourceEntityVelocityComponent.Velocity.X) == 1;
                bool travelingLeft = Math.Sign(sourceEntityVelocityComponent.Velocity.X) == -1;

                if (!travelingUp && !travelingDown && !travelingRight && !travelingLeft) return;

                Vector2 displacement = Vector2.Zero;

                float upWeight = 0f;
                float downWeight = 0f;
                float rightWeight = 0f;
                float leftWeight = 0f;


                //determine direction of travel

                if (travelingUp)
                {
                    displacement.Y = Math.Abs(targetRectangle.Bottom - sourceRectangle.Top);
                    upWeight = displacement.Y / Math.Abs(sourceEntityVelocityComponent.Velocity.Y);
                }
                if (travelingDown)
                {
                    displacement.Y = Math.Abs(sourceRectangle.Top - targetRectangle.Bottom);
                    downWeight = displacement.Y / Math.Abs(sourceEntityVelocityComponent.Velocity.Y);
                }
                if (travelingRight)
                {
                    displacement.X = Math.Abs(targetRectangle.Right - sourceRectangle.Left);
                    rightWeight = displacement.X / Math.Abs(sourceEntityVelocityComponent.Velocity.X);
                }
                if (travelingLeft)
                {
                    displacement.X = Math.Abs(sourceRectangle.Left - targetRectangle.Right);
                    leftWeight = displacement.X / Math.Abs(sourceEntityVelocityComponent.Velocity.X);
                }

                //correct based on direction

                if (leftWeight > rightWeight && leftWeight > upWeight && leftWeight > downWeight)
                {
                    sourceEntityPositionComponent.X = targetRectangle.Right + 11;
                }
                else if (rightWeight > leftWeight && rightWeight > upWeight && rightWeight > downWeight)
                {
                    sourceEntityPositionComponent.X = 
                        targetRectangle.Left - targetEntityBoundingBoxComponent.Width - 11;
                }
                else if (upWeight > leftWeight && upWeight > rightWeight && upWeight > downWeight)
                {
                    sourceEntityPositionComponent.Y = targetRectangle.Bottom + 11;
                }
                else if (downWeight > leftWeight && downWeight > rightWeight && downWeight > upWeight)
                {
                    sourceEntityPositionComponent.Y = 
                        targetRectangle.Top  - targetEntityBoundingBoxComponent.Height - 11;
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
