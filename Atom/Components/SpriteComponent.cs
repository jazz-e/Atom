using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Microsoft.Xna.Framework.Graphics;

namespace Atom.Components
{
    public class SpriteComponent : Component
    {
        public Texture2D Image
        {
            get;
            set;
        }
        public int FrameWidth
        {
            get;
            set;
        }
        public int FrameHeight
        {
            get;
            set;
        }
        public int Scale
        {
            get;
            set;
        }
        public Point Location
        {
            get;
            set;
        }
    }
}
