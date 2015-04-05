using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Physics.Collision.BoundingBox
{
    public class BoundingBoxComponent : CollisionComponent
    {
        public int RelativeX { get; set; }
        public int RelativeY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool ShouldMoveY { get; set; }
    }
}
