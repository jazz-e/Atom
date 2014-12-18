using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Atom.Input;
using Atom.Physics;

namespace Atom.Graphics
{
    public class GameObject : IEntity
    {
        #region Properties

        public Vector2 Position;

        public Single Rotation;
        public Vector2 Origin;
        public Single Scale;
        public Single LayerDepth;

        #region Components

        public InputComponent Controller;
        public IRender Renderer;
        public PhysicsComponent Physics;

        #endregion

        #endregion

        public virtual void Update(GameTime gameTime)
        {
            if (Renderer != null)
                Renderer.Update(gameTime);

            if (Controller != null)
                Controller.Update(gameTime);

            if (Physics != null)
                Physics.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            if(Renderer != null)
                Renderer.Draw(spritebatch);
        }
    }
}
