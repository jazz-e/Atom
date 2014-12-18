using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Atom.Physics;

namespace Atom.Input
{
    public class KeyboardPlayerInput : InputComponent
    {
        #region Protected Fields

        protected KeyboardState _keyboard = Keyboard.GetState();

        protected EntityPhysics _entityPhysics;

        #endregion

        public KeyboardPlayerInput(EntityPhysics entityPhysics) : base(entityPhysics){}

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            if(_keyboard.IsKeyDown(Keys.W))
            {

            }

            if (_keyboard.IsKeyDown(Keys.A))
            {

            }

            if (_keyboard.IsKeyDown(Keys.S))
            {

            }

            if (_keyboard.IsKeyDown(Keys.D))
            {

            }

            base.Update(gameTime);
        }

    }
}
