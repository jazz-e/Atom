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

                // 0 the axis with the largest mtd value.
                if (Math.Abs(MinimumTranslationDistance.X) < Math.Abs(MinimumTranslationDistance.Y))
                    MinimumTranslationDistance.Y = 0;
                else
                    MinimumTranslationDistance.X = 0;

                sourceEntityPositionComponent.X += MinimumTranslationDistance.X;
                sourceEntityPositionComponent.Y += MinimumTranslationDistance.Y;
            }
        }

        public TypeFilter GetMessageTypeFilter()
        {
            return new TypeFilter()
                .AddFilter(typeof (CollisionMessage));
        }
    }
}
