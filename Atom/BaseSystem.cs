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

        /// <summary>
        /// Runs the update logic of the system
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="entityId">The id of the entity to be updated</param>
        public virtual void Update(GameTime gameTime, int entityId) {}

        /// <summary>
        /// Runs the update draw logic of the system
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="entityId">The id of the entity to be drawn</param>
        public virtual void Draw(SpriteBatch spriteBatch, int entityId) {}

        /// <summary>
        /// Removes the component from the system
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityId">The id of the entity</param>
        public virtual void Purge<T>(int entityId)
        {
            _components.RemoveAll(component => component.EntityId == entityId && component.GetType() == typeof(T));
        }

        /// <summary>
        /// Flags the component to not be used by system
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityId">The id of the entity</param>
        public virtual void Disable<T>(int entityId)
        {
            _components
                   .Where(component => component.EntityId == entityId && !component.Disabled)
                   .ToList()
                   .ForEach(component => component.Disabled = true);
        }

        /// <summary>
        /// Flags the component to be used by system
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityId">The id of the entity</param>
        public virtual void Enable<T>(int entityId)
        {
            _components
                    .Where(component => component.EntityId == entityId && component.Disabled)
                    .ToList()
                    .ForEach(component => component.Disabled = false);
        }

        /// <summary>
        /// Removes all components related to the id given
        /// </summary>
        /// <param name="entityId">The id of the entity</param>
        public virtual void RemoveEntityComponents(int entityId)
        {
            _components.RemoveAll(component => component.EntityId == entityId);
        }

        /// <summary>
        /// Adds all the components to the system that match the type filter of the system
        /// </summary>
        /// <param name="entityId">The id of the entity</param>
        /// <param name="components"></param>
        public virtual void AddEntityComponents(int entityId, IEnumerable<Component> components)
        {
            List<Component> entityComponents = components.Where(component => component.EntityId == entityId).ToList();

            List<Component> filterEntityComponents = ComponentTypeFilter.FilterList(entityComponents);

            _components.AddRange(filterEntityComponents);
        }

        /// <summary>
        /// Gets the component that match the entity id and type given
        /// </summary>
        /// <typeparam name="T">The component type to return</typeparam>
        /// <param name="entityId">The id of the entity</param>
        /// <returns></returns>
        protected T GetComponentByEntityId<T>(int entityId) where T : class
        {
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
