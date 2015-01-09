namespace Atom.Graphics
{
    public class AnimatedSpriteComponent : Component 
    {
        /// <summary>
        /// How many frames will be played per second
        /// </summary>
        public int FramesPerSecond { get; set; }

        /// <summary>
        /// The current frame the animation is on
        /// </summary>
        public int FrameIndex { get; set; }

        /// <summary>
        /// How many frames in total are on the spritesheet
        /// </summary>
        public int FrameCount { get; set; }
    }
}
