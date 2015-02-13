using System.Linq;
using Atom.Messaging;
using Atom.Physics.Movement;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Gravity
{
    public class GravitySystem : BaseSystem
    {

        public GravitySystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof (GravityComponent))
                .AddFilter(typeof (MassComponent));
        }

        public override void Update(GameTime gameTime, int entityId)
        {
            GravityComponent gravity =
                GetComponentsByEntityId<GravityComponent>(entityId).FirstOrDefault();

            MassComponent mass =
                GetComponentsByEntityId<MassComponent>(entityId).FirstOrDefault();

            if (gravity == null || mass == null) return;

            MoveMessage moveMessage = new MoveMessage(entityId, new Vector2(0, gravity.Gravity * mass.Mass));

            PostOffice.SendMessage(moveMessage);
        }
    }
}
