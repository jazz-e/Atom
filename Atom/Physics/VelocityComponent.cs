using Microsoft.Xna.Framework;

namespace Atom.Physics
{
    public class VelocityComponent : Component
    {
        /// <summary>
        /// The X and Y Velocity of the entity
        /// </summary>
        public Vector2 Velocity { get; set; }

        public Vector2 PreviousVelocity { get; set; }

        public float X
        {
            get { return Velocity.X; }
            set { Velocity = new Vector2(value, Velocity.Y); }
        }

        public float Y
        {
            get { return Velocity.Y; }
            set { Velocity = new Vector2(Velocity.X, value); }
        }
    }
}
