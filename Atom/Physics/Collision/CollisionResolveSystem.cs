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


                bool travellingUp = Math.Sign(sourceEntityVelocityComponent.PreviousVelocity.Y) == -1;
                bool travellingDown = Math.Sign(sourceEntityVelocityComponent.PreviousVelocity.Y) == 1;
                bool travellingRight = Math.Sign(sourceEntityVelocityComponent.PreviousVelocity.X) == 1;
                bool travellingLeft = Math.Sign(sourceEntityVelocityComponent.PreviousVelocity.X) == -1;

                //if (!travelingUp && !travelingDown && !travelingRight && !travelingLeft) return;

                Rectangle overlapRectangle = new Rectangle();

                float timeY = 0f;
                float timeX = 0F;

                float newSourceX = sourceEntityPositionComponent.X;
                float newSourceY = sourceEntityPositionComponent.Y;

                if (travellingUp)
                {
                    overlapRectangle.Y = sourceRectangle.Y;
                    overlapRectangle.Height = Math.Abs(targetRectangle.Bottom - sourceRectangle.Top);

                    timeY = overlapRectangle.Y / Math.Abs(sourceEntityVelocityComponent.PreviousVelocity.Y);

                    newSourceY += overlapRectangle.Height;

                    sourceEntityPositionComponent.Y += sourceEntityVelocityComponent.PreviousVelocity.Y * -1;
                }
                
                if (travellingDown)
                {
                    overlapRectangle.Y = targetRectangle.Y;
                    overlapRectangle.Height = Math.Abs(sourceRectangle.Bottom - targetRectangle.Top);

                    timeY = overlapRectangle.Y / Math.Abs(sourceEntityVelocityComponent.PreviousVelocity.Y);

                    newSourceY -= overlapRectangle.Height;

                    sourceEntityPositionComponent.Y -= sourceEntityVelocityComponent.PreviousVelocity.Y * -1;
                }

                if (travellingRight)
                {
                    overlapRectangle.X = targetRectangle.X;
                    overlapRectangle.Width = Math.Abs(sourceRectangle.Right - targetRectangle.Left);

                    timeX = overlapRectangle.X / Math.Abs(sourceEntityVelocityComponent.PreviousVelocity.X);

                    newSourceX -= overlapRectangle.Width;

                    sourceEntityPositionComponent.X -= sourceEntityVelocityComponent.PreviousVelocity.X * -1;
                }
                
                if (travellingLeft)
                {
                    overlapRectangle.X = sourceRectangle.X;
                    overlapRectangle.Width = Math.Abs(sourceRectangle.Left - targetRectangle.Right);

                    timeX = overlapRectangle.X / Math.Abs(sourceEntityVelocityComponent.PreviousVelocity.X);

                    newSourceX += overlapRectangle.Width;

                    sourceEntityPositionComponent.X += sourceEntityVelocityComponent.PreviousVelocity.X*-1;
                }

                Console.WriteLine("TimeX: " + timeX);
                Console.WriteLine("TimeY: " + timeY);

                if (timeX > timeY)
                {
                    //sourceEntityPositionComponent.X = newSourceX;
                }

                if (timeY > timeX)
                {
                    //sourceEntityPositionComponent.Y = newSourceY;
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
