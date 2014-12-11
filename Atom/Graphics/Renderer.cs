using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Atom.Graphics
{
    public class Renderer : IRender
    {
        protected Texture2D _image;
        public int width
        {
            get;
            set;
        }
        public int height
        {
            get;
            set;
        }
        
        protected GameObject _gameObject;

        public Renderer(GameObject gameObject, ContentManager content, string assetName)
        {
            _image = content.Load<Texture2D>(assetName);

            if (_image != null)
            {
                this.width = _image.Width;
                this.height = _image.Height;
            }

            _gameObject = gameObject;
            _gameObject.origin = new Vector2(this.width / 2, this.height / 2);
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(_gameObject.origin == new Vector2(0,0) &&
                _gameObject.rotation == 0)
            {
            spriteBatch.Draw(_image,
                new Rectangle((int)_gameObject.position.X,
                    (int)_gameObject.position.Y,
                    width, height),
                Color.White);
            }
            else 
            {
            spriteBatch.Draw(_image,
                _gameObject.position,
                null,
                Color.White,
                _gameObject.rotation,
                _gameObject.origin,
                _gameObject.scale,
                SpriteEffects.None,
                _gameObject.layerDepth);
             }
        }
    }
}
