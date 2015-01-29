using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Physics.Collision
{
    public abstract class CollisionComponent : Component
    {
        public bool Active { get; set; }
    }
}
