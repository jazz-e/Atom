using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Physics.Collision.BoundingBox
{
    public class BoundingBoxComponent : CollisionComponent
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
