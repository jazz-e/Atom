using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atom.Messaging;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Collision.BoundingBox
{
    public class BoundingBoxSystem : BaseSystem
    {
        public BoundingBoxSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof (BoundingBoxComponent))
                .AddFilter(typeof (PositionComponent));
        }

        public override void Update(GameTime gameTime, int entityId)
        {
            BoundingBoxComponent boundingBoxComponent = 
                GetComponentsByEntityId<BoundingBoxComponent>(entityId).FirstOrDefault();

            PositionComponent positionComponent =
                GetComponentsByEntityId<PositionComponent>(entityId).FirstOrDefault();

            if (boundingBoxComponent == null || !boundingBoxComponent.Active || positionComponent == null) return;

            List<BoundingBoxComponent> otherBoundingBoxComponents = _components.FindAll(
                component => component.EntityId != entityId 
                    && component.GetType() == typeof (BoundingBoxComponent))
                    .Cast<BoundingBoxComponent>()
                    .ToList();

            Rectangle boundingBox = new Rectangle((int) positionComponent.X, (int) positionComponent.Y,
                boundingBoxComponent.Width, boundingBoxComponent.Height);


            foreach (BoundingBoxComponent otherBoundingBoxComponent in otherBoundingBoxComponents)
            {
                PositionComponent otherPositionComponent =
                    GetComponentsByEntityId<PositionComponent>(otherBoundingBoxComponent.EntityId).FirstOrDefault();

                if (otherPositionComponent == null) continue;

                Rectangle otherBoundingBox = new Rectangle((int) otherPositionComponent.X, (int) otherPositionComponent.Y,
                    otherBoundingBoxComponent.Width, otherBoundingBoxComponent.Height);


                if (boundingBox.Intersects(otherBoundingBox))
                {
                    PostOffice.SendMessage(new CollisionMessage(entityId, otherBoundingBoxComponent.EntityId));
                }
            }
        }
    }
}
