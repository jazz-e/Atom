using Atom.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Atom.World
{
    public class World
    {
        public List<GameObject> GameObjects = new List<GameObject>();

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update(gameTime);
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
        }

        public List<T> GetGameObjects<T>() where T : GameObject 
        {
            List<T> matches = new List<T>();

            foreach (GameObject gameObject in GameObjects)
            {
                if (typeof (T) == gameObject.GetType())
                {
                    matches.Add((T)gameObject);
                }
            }

            return matches;

        }
    }
}
