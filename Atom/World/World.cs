using System;
using System.Collections.Generic;
using System.Linq;
using Atom.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.World
{
    public class World
    {
        private List<System> _systems = new List<System>();
        private HashSet<BaseEntity> _entities = new HashSet<BaseEntity>();

        public bool Paused { get; set; }

        public World()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if (Paused)
                return;

            foreach (System system in _systems)
            {
                foreach (BaseEntity entity in _entities)
                {
                    system.Update(gameTime, entity.Id);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (System system in _systems)
            {
                foreach (BaseEntity entity in _entities)
                {
                    system.Draw(spriteBatch, entity.Id);
                }
            }
        }

        public void AddSystem(System system)
        {
            if (system == null)
                throw new ArgumentNullException();

            _systems.Add(system);
        }

        public void RemoveSystem<T>() where T : System
        {
            _systems.RemoveAll(system => system.GetType() == typeof (T));
        }

        public BaseEntity AddEntity(BaseEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            if (_entities.Where(ent => ent.Id == entity.Id).Count() > 0)
                entity.Id = EntityFactory.GetInstance().GetNextEntityId();

            _entities.Add(entity);

            return entity;
        }

        public int RemoveEntity(int id)
        {
            return _entities.RemoveWhere(entity => entity.Id == id);
        }

        public BaseEntity GetEntity(int id)
        {
            return _entities.Where(entity => entity.Id == id).First();
        }
    }
}
