using Microsoft.Xna.Framework;

namespace Atom.Physics
{
    public class MassComponent : Component
    {
        /// <summary>
        /// The Mass of the entity in kg.
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// TODO Move to own component
        /// </summary>
        public Vector2 Force { get; set; }
    }
}
