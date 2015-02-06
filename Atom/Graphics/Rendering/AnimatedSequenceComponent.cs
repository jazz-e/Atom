using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atom.Graphics.Rendering
{
   public class AnimatedSequenceComponent : Component
    {
        /// <summary>
        /// What is the current direction the animation is play in
        /// </summary>
        public SequenceDirection CurrentSequenceDirection 
            = SequenceDirection.NONE;

        /// <summary>
        /// What are the current frames you want to use
        /// </summary>
        public int[] AnimationSequence = null;

    }
}
