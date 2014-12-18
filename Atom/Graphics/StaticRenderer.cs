using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atom.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Atom.Graphics
{
    public class StaticRenderer : IRender
    {
        protected Texture2D Image;
        public int Width
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }
        
        protected GameObject GameObject;

        public StaticRenderer(GameObject gameObject, ContentManager content, string assetName)
        {
            Image = content.Load<Texture2D>(assetName);

            if (Image != null)
            {
                this.Width = Image.Width;
                this.Height = Image.Height;
            }

            GameObject = gameObject;
            GameObject.Origin = new Vector2(this.Width / 2, this.Height / 2);
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(GameObject.Origin == new Vector2(0,0) &&
                GameObject.Rotation == 0)
            {
            spriteBatch.Draw(Image,
                new Rectangle((int)GameObject.Position.X,
                    (int)GameObject.Position.Y,
                    Width, Height),
                Color.White);
            }
            else 
            {
            spriteBatch.Draw(Image,
                GameObject.Position,
                null,
                Color.White,
                GameObject.Rotation,
                GameObject.Origin,
                GameObject.Scale,
                SpriteEffects.None,
                GameObject.LayerDepth);
             }
        }
    }
}
