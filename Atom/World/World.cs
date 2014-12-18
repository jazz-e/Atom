using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using Atom.Graphics;

namespace Atom.World
{
    public class World
    {
        List<GameObject> GameObjects = new List<GameObject>();

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public List<T> GetGameObjects<T>() where T : GameObject 
        {
            
            return (List<T>)GameObjects.FindAll(x => x.GetType() == );
        }
    }
}
