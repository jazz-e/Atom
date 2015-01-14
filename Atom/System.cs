using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom
{
    public abstract class System
    {
        public virtual void Update(GameTime gameTime, int entityId) {}

        public virtual void Draw(SpriteBatch spriteBatch, int entityId) {}

        public virtual void Purge<T>(int entityId) where T : Component {}

        public virtual void Disable<T>(int entityId) where T : Component {}

        public virtual void Enable<T>(int entityId) where T : Component {}
    }
}
