using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom
{
    public abstract class BaseSystem
    {
        protected List<Component> _components = new List<Component>();

        protected TypeFilter ComponentTypeFilter;

        public virtual void Update(GameTime gameTime, int entityId) {}

        public virtual void Draw(SpriteBatch spriteBatch, int entityId) {}

        public virtual void Purge<T>(int entityId)
        {
            _components.RemoveAll(component => component.EntityId == entityId && component.GetType() == typeof(T));
        }

        public virtual void Disable<T>(int entityId)
        {
            _components
                   .Where(component => component.EntityId == entityId && !component.Disabled)
                   .ToList()
                   .ForEach(component => component.Disabled = true);
        }

        public virtual void Enable<T>(int entityId)
        {
            _components
                    .Where(component => component.EntityId == entityId && component.Disabled)
                    .ToList()
                    .ForEach(component => component.Disabled = false);
        }

        public virtual void RemoveEntityComponents(int entityId)
        {
            _components.RemoveAll(component => component.EntityId == entityId);
        }

        public virtual void AddEntityComponents(int entityId, IEnumerable<Component> components)
        {
            List<Component> entityComponents = components.Where(component => component.EntityId == entityId).ToList();

            List<Component> filterEntityComponents = ComponentTypeFilter.FilterList(entityComponents);

            _components.AddRange(filterEntityComponents);
        }

        protected T GetComponentByEntityId<T>(int entityId) where T : class
        {
            //return _components.Where(component => component.EntityId == entityId && !component.Disabled) as T;

            foreach (Component component in _components)
            {
                if (component.GetType() == typeof (T) && !component.Disabled && component.EntityId == entityId)
                {
                    return component as T;
                }
            }

            return null;
        }
    }
}
