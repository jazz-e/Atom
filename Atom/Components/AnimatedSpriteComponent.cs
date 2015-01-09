using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Components
{
    public class AnimatedSpriteComponent : Component 
    {
        public int FramesPerSecond
        {
            get;
            set;
        }
        public int FrameIndex
        {
            get;
            set;
        }
        public int FrameCount
        {
            get;
            set;
        }
    }
}
