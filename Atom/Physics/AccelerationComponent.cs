using Microsoft.Xna.Framework;

namespace Atom.Physics
{
    public class AccelerationComponent :  Component
    {
        public Vector2 Acceleration { get; set; }

        public Vector2 PreviousAcceleration { get; set; }
    }
}
