using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.GUI
{
    public class GuiLabel : BaseGui
    {
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public float FontScale { get; set; }
        public Color FontColour { get; set; }

        #region Constructors
        public GuiLabel(Texture2D texture, Point location, int width, int height, bool visible = true) : base(texture, location, width, height, visible)
        {
        }

        public GuiLabel(Point location, int width, int height) : base(location, width, height)
        {
        }

        public GuiLabel(Texture2D texture, Vector2 location, float width, float height, bool visible = true) : base(texture, location, width, height, visible)
        {
        }

        public GuiLabel(Vector2 location, float width, float height) : base(location, width, height)
        {
        }

        public GuiLabel(Texture2D texture, Vector2 location, float width, float height, int maxWidth, int maxHeight) : base(texture, location, width, height, maxWidth, maxHeight)
        {
        }

        public GuiLabel(Vector2 location, float width, float height, int maxWidth, int maxHeight) : base(location, width, height, maxWidth, maxHeight)
        {
        }
        #endregion

        public override void Update()
        {
            SetWidth((Font.MeasureString(Text).X * FontScale) / ParentContainer.Width);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(Font, Text, new Vector2(Location.X, Location.Y), FontColour, 0, Vector2.Zero, FontScale, SpriteEffects.None, 0);
        }
    }
}
