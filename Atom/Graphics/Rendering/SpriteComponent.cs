using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.Graphics.Rendering
{
    public class SpriteComponent : Component
    {
        /// <summary>
        /// The image/spritesheet to draw from
        /// </summary>
        public Texture2D Image { get; set; }

        /// <summary>
        /// The width of the current frame to be drawn
        /// </summary>
        public int FrameWidth { get; set; }

        /// <summary>
        /// The height of the current frame to be drawn
        /// </summary>
        public int FrameHeight { get; set; }

        /// <summary>
        /// The scale at which to draw the frame at
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// The top right location on the spritesheet to take the frame from
        /// </summary>
        public Point Location { get; set; }
    }
}
