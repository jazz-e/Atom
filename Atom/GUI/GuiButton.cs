using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.GUI
{
    public class GuiButton : BaseGui, IClickable
    {
        public State CurrentState { get; set; }

        public Texture2D HoverTexture { get; set; }
        public Texture2D ClickedTexture { get; set; }
        private Texture2D _defaultTexture;
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Color FontColour { get; set; }
        public float FontScale { get; set; }


        #region Constructors

        public GuiButton(Point location, int width, int height) : this(null, location, width, height)
        {
        }

        public GuiButton(Texture2D texture, Point location, int width, int height) : base(texture, location, width, height)
        {
        }

        public GuiButton(Vector2 location, float width, float height) : base(location, width, height)
        {
        }

        #endregion

        public event OnClickEvent OnClickHandler;
        public event OnPressEvent OnPressHandler;
        public event OnReleaseEvent OnReleaseHandler;
        public event OnEnterEvent OnEnterHandler;
        public event OnLeaveEvent OnLeaveHandler;
        public event WhileDownEvent WhileDownHandler;
        public event WhileHoverEvent WhileHoverHandler;

        public void OnClick(OnClickEventArgs e)
        {
            if (OnClickHandler != null)
                OnClickHandler.Invoke(this, e);
        }

        public void OnPress(OnPressEventArgs e)
        {
            Texture = ClickedTexture;
            if(OnPressHandler != null)
                OnPressHandler.Invoke(this, e);
        }

        public void OnRelease(OnReleaseEventArgs e)
        {
            Texture = HoverTexture;
            if(OnReleaseHandler != null)
                OnReleaseHandler.Invoke(this, e);
        }

        public void OnEnter(OnEnterEventArgs e)
        {
            _defaultTexture = Texture;
            Texture = HoverTexture;
            if(OnEnterHandler != null)
                OnEnterHandler.Invoke(this, e);
        }

        public void OnLeave(OnLeaveEventArgs e)
        {
            Texture = _defaultTexture;
            if(OnLeaveHandler != null)
                OnLeaveHandler.Invoke(this, e);
        }

        public void WhileHeld(WhileDownEventArgs e)
        {
            if(WhileDownHandler != null)
                WhileDownHandler.Invoke(this, e);
        }

        public void WhileHovering(WhileHoverEventArgs e)
        {
            if(WhileHoverHandler != null)
                WhileHoverHandler.Invoke(this, e);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            

            float fontHeight = Font.MeasureString(Text).Y * FontScale;
            float fontWidth = Font.MeasureString(Text).X * FontScale;
            

            spriteBatch.DrawString(Font, Text, new Vector2(Location.X - (fontWidth /2) + (Width/2), Location.Y - (fontHeight / 2) + (Height/2)), FontColour, 0, Vector2.Zero, FontScale, SpriteEffects.None, 0);

        }
    }
}
