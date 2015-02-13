using Microsoft.Xna.Framework;

namespace Atom.Physics
{
    public class PositionComponent : Component
    {
        /// <summary>
        /// The X position of the entity in the world
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The Y position of the entity in the world
        /// </summary>
        public float Y { get; set; }

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
    }
}
