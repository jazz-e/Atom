namespace Atom.Graphics.Rendering
{
    public class AnimatedSpriteComponent : SpriteComponent 
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

        /// <summary>
        /// Where do you want to start the animation sequence
        /// </summary>
        public int SequenceStartFrame { get; set; }
    }
}
