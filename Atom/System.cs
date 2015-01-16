using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom
{
    public abstract class System
    {
        public abstract void Update(GameTime gameTime, int entityId);

        public abstract void Draw(SpriteBatch spriteBatch, int entityId);

        public abstract void Purge<T>(int entityId) where T : Component;

        public abstract void Disable<T>(int entityId) where T : Component;

        public abstract void Enable<T>(int entityId) where T : Component;

        public abstract void RemoveEntityComponents(int entityId);

        public abstract void AddComponent(Component component);
    }
}
