using Atom.World;
using Microsoft.Xna.Framework;

namespace Atom.Physics.Components
{
    public class CollisionPhysics
    {
        protected World.World World;

        public CollisionPhysics(World.World world)
        {
            World = world;
        }


        public virtual void Update(GameTime gameTime, GameObject gameObject, Vector2 velocity)
        {
            

        }

    }
}
