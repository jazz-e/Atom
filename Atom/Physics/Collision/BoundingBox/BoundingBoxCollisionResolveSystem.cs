﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Atom.Entity;
using Atom.World;
using Atom.Messaging;
using Atom.Physics.Gravity;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Collision.BoundingBox
{
    public class BoundingBoxCollisionResolveSystem : BaseSystem, IReceiver
    {
        public BoundingBoxCollisionResolveSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof (VelocityComponent))
                .AddFilter(typeof (PositionComponent))
                .AddFilter(typeof (BoundingBoxComponent))
                .AddFilter(typeof (MassComponent))
                .AddFilter(typeof (GravityComponent))
                .AddFilter(typeof (AccelerationComponent))
                .AddFilter(typeof (CollisionExclusionComponent));

            PostOffice.Subscribe(this);
        }

        public void OnMessage(IMessage message)
        {
            if (message.GetType() == typeof(CollisionMessage))
            {
                CollisionMessage collisionMessage = (CollisionMessage)message;

                BaseEntity entity = Atom.World.World.GetInstance().GetEntity(collisionMessage.GetTargetCollidable());

                CollisionExclusionComponent exclusionComponent =
                    GetComponentsByEntityId<CollisionExclusionComponent>(collisionMessage.GetSourceCollidable()).FirstOrDefault();

                if (exclusionComponent != null)
                {
                    if (exclusionComponent.Exclusions.Contains(entity.GetType()))
                    {
                        return;
                    }
                }

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

                CollisionFace collisionFace;

                // 0 the axis with the largest mtd value.
                if (Math.Abs(MinimumTranslationDistance.X) <= Math.Abs(MinimumTranslationDistance.Y))
                {
                    MinimumTranslationDistance.Y = 0;

                    collisionFace = CollisionFace.Left;

                    if (MinimumTranslationDistance.X > 0)
                    {
                        collisionFace = CollisionFace.Right;
                    }
                }
                else
                {
                    MinimumTranslationDistance.X = 0;

                    collisionFace = CollisionFace.Bottom;

                    if (MinimumTranslationDistance.Y > 0)
                    {
                        collisionFace = CollisionFace.Top;
                    }
                }

                CollisionFace targetFace;

                Enum.TryParse((-Convert.ToInt32(collisionFace)).ToString(CultureInfo.InvariantCulture), out targetFace);

                sourceEntityPositionComponent.Position += MinimumTranslationDistance;

                PostOffice.SendMessage(new CollisionResponseMessage(sourceVelocityComponent.EntityId, collisionFace));
                PostOffice.SendMessage(new CollisionResponseMessage(targetEntityPositionComponent.EntityId, targetFace));
            }
        }
        
        public TypeFilter GetMessageTypeFilter()
        {
            return new TypeFilter()
                .AddFilter(typeof(CollisionMessage));
        }
    }
}
