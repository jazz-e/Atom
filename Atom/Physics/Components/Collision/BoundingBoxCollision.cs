using Microsoft.Xna.Framework;

namespace Atom.Physics.Components.Collision
{
    public class BoundingBoxCollision 
    {
        public bool CollisionCheck(IBoundingBoxCollidable firstColliable, IBoundingBoxCollidable secondCollidable)
        {
            return firstColliable.BoundingBox.Intersects(secondCollidable.BoundingBox);
        }
    }
}
