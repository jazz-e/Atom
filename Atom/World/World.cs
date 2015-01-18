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
        private List<BaseSystem> _systems = new List<BaseSystem>();
        private HashSet<BaseEntity> _entities = new HashSet<BaseEntity>();

        public bool Paused { get; set; }

        public World()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if (Paused)
                return;

            foreach (BaseSystem system in _systems)
            {
                foreach (BaseEntity entity in _entities)
                {
                    system.Update(gameTime, entity.Id);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (BaseSystem system in _systems)
            {
                foreach (BaseEntity entity in _entities)
                {
                    system.Draw(spriteBatch, entity.Id);
                }
            }
        }

        public void AddSystem(BaseSystem system)
        {
            if (system == null)
                throw new ArgumentNullException();

            _systems.Add(system);
        }

        public void RemoveSystem<T>() where T : BaseSystem
        {
            _systems.RemoveAll(system => system.GetType() == typeof (T));
        }

        public BaseEntity AddEntity(BaseEntity entity, List<Component> components)
        {
            if (entity == null)
                throw new ArgumentNullException();

            if (_entities.Where(ent => ent.Id == entity.Id).Count() > 0)
                entity.Id = EntityFactory.GetInstance().GetNextEntityId();

            _entities.Add(entity);

            foreach (BaseSystem system in _systems)
            {
                foreach (Component component in components)
                {
                    system.AddComponent(component);
                }
            }
            return entity;
        }

        public void RemoveEntity(int id)
        {
            IEnumerable<BaseEntity> removeEntities = _entities.Where(entity => entity.Id == id);

            foreach (BaseEntity entity in removeEntities)
            {
                foreach (BaseSystem system in _systems)
                {
                    system.RemoveEntityComponents(id);
                }
            }
        }

        public BaseEntity GetEntity(int id)
        {
            return _entities.Where(entity => entity.Id == id).First();
        }
    }
}
