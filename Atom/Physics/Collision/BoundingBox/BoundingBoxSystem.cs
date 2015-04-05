using System.Collections.Generic;
using System.Linq;
using Atom.Entity;
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
                .AddFilter(typeof (PositionComponent))
                .AddFilter(typeof (CollisionExclusionComponent));
        }

        public override void Update(GameTime gameTime, int entityId)
        {
            BoundingBoxComponent boundingBoxComponent = 
                GetComponentsByEntityId<BoundingBoxComponent>(entityId).FirstOrDefault();

            PositionComponent positionComponent =
                GetComponentsByEntityId<PositionComponent>(entityId).FirstOrDefault();

            if (boundingBoxComponent == null || !boundingBoxComponent.Active || positionComponent == null) 
                return;

            BaseEntity entity = Atom.World.World.GetInstance().GetEntity(entityId);

            CollisionExclusionComponent exclusionComponent =
                GetComponentsByEntityId<CollisionExclusionComponent>(entityId).FirstOrDefault();



            List<BoundingBoxComponent> otherBoundingBoxComponents =
                GetAllComponentOfTypeExcept<BoundingBoxComponent>(entityId);

            if (exclusionComponent != null)
            {
                List<int> entityIds = otherBoundingBoxComponents.ToDictionary(component => component.EntityId).Keys.ToList();
                List<BaseEntity> entities = new List<BaseEntity>();
                entityIds.ForEach(index => entities.Add(World.World.GetInstance().GetEntity(index)));
                entities = exclusionComponent.Exclusions.ExcludeList(entities);
                List<BoundingBoxComponent> newOtherBoundingBoxComponents = new List<BoundingBoxComponent>();
                entities.ForEach(ent => newOtherBoundingBoxComponents.AddRange(GetComponentsByEntityId<BoundingBoxComponent>(ent.Id)));
                otherBoundingBoxComponents = newOtherBoundingBoxComponents;
            }

            Rectangle boundingBox = new Rectangle((int) positionComponent.X + boundingBoxComponent.RelativeX, (int) positionComponent.Y + boundingBoxComponent.RelativeY,
                boundingBoxComponent.Width, boundingBoxComponent.Height);


            foreach (BoundingBoxComponent otherBoundingBoxComponent in otherBoundingBoxComponents)
            {
                PositionComponent otherPositionComponent =
                    GetComponentsByEntityId<PositionComponent>(otherBoundingBoxComponent.EntityId).FirstOrDefault();

                if (otherPositionComponent == null) continue;

                Rectangle otherBoundingBox = new Rectangle((int) otherPositionComponent.X + otherBoundingBoxComponent.RelativeX, (int) otherPositionComponent.Y + otherBoundingBoxComponent.RelativeY,
                    otherBoundingBoxComponent.Width, otherBoundingBoxComponent.Height);


                if (boundingBox.Intersects(otherBoundingBox))
                {
                    PostOffice.SendMessage(new CollisionMessage(entityId, otherBoundingBoxComponent.EntityId, (float) gameTime.ElapsedGameTime.TotalSeconds));
                }
            }
        }
    }
}
