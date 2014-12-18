using Microsoft.Xna.Framework;

namespace Atom.Physics.Components.Collision
{
    public interface IBoundingBoxCollidable
    {
        Rectangle BoundingBox { get; set; }
        Vector2 Position { get; set; }
    }
}
