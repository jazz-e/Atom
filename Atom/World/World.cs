using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Atom.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom.World
{
    public class World
    {
        private List<BaseSystem> _systems = new List<BaseSystem>();
        private Dictionary<int, BaseEntity> _entities = new Dictionary<int, BaseEntity>();
        private List<BaseEntity> _newEntities = new List<BaseEntity>();
        private List<List<Component>> _newComponents = new List<List<Component>>();
        private List<int> _removeEntities = new List<int>(); 
        private static World _instance;

        public bool Paused { get; set; }

        public World()
        {
            _instance = this;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (_newEntities.Count > 0)
            {
                for (int index = 0; index < _newEntities.Count; index++)
                {
                    BaseEntity entity = _newEntities[index];
                    List<Component> components = _newComponents[index];

                    addEntity(entity, components);
                }

                _newEntities = new List<BaseEntity>();
                _newComponents = new List<List<Component>>();
            }

            if (_removeEntities.Any())
            {
                foreach (int entityId in _removeEntities)
                {
                    foreach (BaseSystem system in _systems)
                    {
                        system.RemoveEntityComponents(entityId);
                    }
                    _entities.Remove(entityId);
                }

                _removeEntities = new List<int>();
            }


            if (Paused)
                return;

            foreach (BaseSystem system in _systems)
            {
                foreach (BaseEntity entity in _entities.Values)
                {
                    system.Update(gameTime, entity.Id);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (BaseSystem system in _systems)
            {
                foreach (BaseEntity entity in _entities.Values)
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

        private BaseEntity addEntity(BaseEntity entity, List<Component> components)
        {
            if (entity == null)
                throw new ArgumentNullException();

            _entities.Add(entity.Id, entity);
            
            AddEntityComponents(entity.Id, components);
            
            return entity;
        }

        public BaseEntity AddEntity(BaseEntity entity, List<Component> components)
        {
            if (entity == null)
                throw new ArgumentNullException();

            if (_entities.Count(ent => ent.Key == entity.Id) > 0)
                entity.Id = EntityFactory.GetInstance().GetNextEntityId();

            _newEntities.Add(entity);
            _newComponents.Add(components);

            return entity;
        }

        public void AddEntityComponents(int entityId, List<Component> components)
        {
            foreach (BaseSystem system in _systems)
            {
                system.AddEntityComponents(entityId, components);
            }
        }

        public void RemoveEntity(int id)
        {
            if (!_entities.ContainsKey(id)) 
                return;

            _removeEntities.Add(id);
        }

        public BaseEntity GetEntity(int id)
        {
            return _entities[id];
        }

        public List<T> GetEntitiesByType<T>()
        {
            return _entities.Values.Where(entity => entity.GetType() == typeof (T)).Cast<T>().ToList();
        }

        public static World GetInstance()
        {
            return _instance;
        }

        public void Destroy()
        {
            foreach (var baseEntity in _entities)
            {
                foreach (var baseSystem in _systems)
                {
                    baseSystem.RemoveEntityComponents(baseEntity.Key);
                }
            }
            _entities.Clear();
            _systems.Clear();
            _instance = null;
        }
    }
}
