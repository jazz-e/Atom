using System;
using Atom.Graphics;
using Atom.Input;
using Atom.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.World
{
    public class GameObject 
    {
        #region Fields

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

        public GameObject(Vector2 position)
        {
            Position = position;
        }

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
