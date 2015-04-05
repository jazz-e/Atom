using System;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.GUI
{
    public delegate void OnDrawEvent(object sender, DrawEventArgs args);

    public sealed class DrawEventArgs : EventArgs
    {
        public SpriteBatch SpriteBatch;

        public DrawEventArgs(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
        }
    }
}