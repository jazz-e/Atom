using System;
using System.Linq;
using Atom.Messaging;
using Atom.Physics.Movement;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Collision.BoundingBox
{
    public class BoundingBoxCollisionResponseSystem : BaseSystem, IReceiver
    {
        public BoundingBoxCollisionResponseSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof(VelocityComponent))
                .AddFilter(typeof(PositionComponent))
                .AddFilter(typeof(BoundingBoxComponent));

            PostOffice.Subscribe(this);
        }

        public void OnMessage(IMessage message)
        {
            if (message.GetType() == typeof(CollisionMessage))
            {
                CollisionMessage collisionMessage = (CollisionMessage)message;

                VelocityComponent sourceVelocityComponent =
                    GetComponentsByEntityId<VelocityComponent>(collisionMessage.GetSourceCollidable()).FirstOrDefault();

                VelocityComponent targetVelocityComponent =
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
                    (int)sourceEntityPositionComponent.X,
                    (int)sourceEntityPositionComponent.Y,
                    sourceEntityBoundingBoxComponent.Width,
                    sourceEntityBoundingBoxComponent.Height);

                Rectangle targetRectangle = new Rectangle(
                    (int)targetEntityPositionComponent.X,
                    (int)targetEntityPositionComponent.Y,
                    targetEntityBoundingBoxComponent.Width,
                    targetEntityBoundingBoxComponent.Height);

                Vector2 sourceMinimum = new Vector2(sourceRectangle.X, sourceRectangle.Y);
                Vector2 sourceMaximum = new Vector2(sourceRectangle.Right, sourceRectangle.Bottom);

                Vector2 targetMinimum = new Vector2(targetRectangle.X, targetRectangle.Y);
                Vector2 targetMaximum = new Vector2(targetRectangle.Right, targetRectangle.Bottom);

                Vector2 MinimumTranslationDistance = new Vector2();

                float left = (targetMinimum.X - sourceMaximum.X);
                float right = (targetMaximum.X - sourceMinimum.X);
                float top = (targetMinimum.Y - sourceMaximum.Y);
                float bottom = (targetMaximum.Y - sourceMinimum.Y);

                if (left > 0 || right < 0) throw new Exception("no intersection");
                if (top > 0 || bottom < 0) throw new Exception("no intersection");

                // box intersect. work out the mtd on both x and y axes.
                if (Math.Abs(left) < right)
                    MinimumTranslationDistance.X = left;
                else
                    MinimumTranslationDistance.X = right;

                if (Math.Abs(top) < bottom)
                    MinimumTranslationDistance.Y = top;
                else
                    MinimumTranslationDistance.Y = bottom;

                float b = -0.5F;

                

                // 0 the axis with the largest mtd value.
                if (Math.Abs(MinimumTranslationDistance.X) < Math.Abs(MinimumTranslationDistance.Y))
                {
                    MinimumTranslationDistance.Y = 0;
                    
                    
                }
                else
                {
                    MinimumTranslationDistance.X = 0;
                    
                }

                
                
                
                sourceEntityPositionComponent.Position += MinimumTranslationDistance;
                


                float k = 20F;
               

        

                Vector2 x = Vector2.Subtract(sourceEntityPositionComponent.Position, targetEntityPositionComponent.Position);

                x = Vector2.Negate(x);

                Vector2 v = (sourceVelocityComponent.Velocity - targetVelocityComponent.Velocity);

                Vector2 bv = b*v;

                Vector2 kx = k*x;

                Vector2 F1 = -kx - bv;

                if ((float.IsNaN(F1.X) || float.IsNaN(F1.Y))) return;

               MoveMessage move = new MoveMessage(sourceEntityPositionComponent.EntityId, F1);

               PostOffice.SendMessage(move);

            }
        }
        
        public TypeFilter GetMessageTypeFilter()
        {
            return new TypeFilter()
                .AddFilter(typeof(CollisionMessage));
        }
    }
}
