using System.Collections.Generic;

namespace Atom.Physics.Collision
{
    public class CollisionExclusionComponent : Component
    {
        public TypeFilter Exclusions { get; set; }
    }
}
