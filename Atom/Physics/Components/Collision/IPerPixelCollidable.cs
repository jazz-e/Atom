using Microsoft.Xna.Framework;

namespace Atom.Physics.Components.Collision
{
    public interface IPerPixelCollidable
    {
        Color[] SpriteColors { get; set; }
        Rectangle BoundingBox { get; set; }
        Vector2 Position { get; set; }
    }
}
