using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Atom.Graphics
{
    public class GameObject : IEntity
    {
        // -------------- Properties and Attributes ----------
        public Vector2 position
        {
            get;
            set;
        }
        public int velocity;
        public Single rotation;
        public Vector2 origin;
        public Single scale;
        public Single layerDepth;

        // ------------------ Object Components --------------
        public IRender render
        {
            get;
            set;
        }


        //----------------------- Methods ----------------

        public virtual void Update(GameTime gameTime)
        {
            if (render != null)
                render.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            if(render != null)
                render.Draw(spritebatch);
        }
    }
}
