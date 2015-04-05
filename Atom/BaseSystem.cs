using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Atom
{
    public abstract class BaseSystem
    {
        protected Dictionary<int, List<Component>> _components = new Dictionary<int, List<Component>>();

        protected TypeFilter ComponentTypeFilter;

        /// <summary>
        /// Runs the update logic of the system
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="entityId">The id of the entity to be updated</param>
        public virtual void Update(GameTime gameTime, int entityId)
        {
        }

        /// <summary>
        /// Runs the update draw logic of the system
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="entityId">The id of the entity to be drawn</param>
        public virtual void Draw(SpriteBatch spriteBatch, int entityId) { }

        /// <summary>
        /// Removes the component from the system
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityId">The id of the entity</param>
        public virtual void Purge<T>(int entityId)
        {
            if (_components.ContainsKey(entityId) == false) return;

            List<Component> components = _components[entityId];

            components.RemoveAll(component => component is T);
        }

        /// <summary>
        /// Flags the component to not be used by system
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityId">The id of the entity</param>
        public virtual void Disable<T>(int entityId)
        {
            if (_components.ContainsKey(entityId) == false) return;

            List<Component> components = _components[entityId];

            components
                   .Where(component => !component.Disabled)
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
            if (_components.ContainsKey(entityId) == false) return;

            List<Component> components = _components[entityId];
            components
                    .Where(component => component.Disabled)
                    .ToList()
                    .ForEach(component => component.Disabled = false);
        }

        /// <summary>
        /// Removes all components related to the id given
        /// </summary>
        /// <param name="entityId">The id of the entity</param>
        public virtual void RemoveEntityComponents(int entityId)
        {
            if (_components.ContainsKey(entityId) == false) return;
            _components.Remove(entityId);
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

            if (_components.ContainsKey(entityId))
            {
                _components[entityId].AddRange(components);
            }
            else
            {
                _components.Add(entityId, filterEntityComponents);
            }
        }

        /// <summary>
        /// Gets the components that match the entity id and type given
        /// </summary>
        /// <typeparam name="T">The component type to return</typeparam>
        /// <param name="entityId">The id of the entity</param>
        /// <returns></returns>
        protected List<T> GetComponentsByEntityId<T>(int entityId) where T : class
        {
            if (!_components.ContainsKey(entityId)) return new List<T>();

            List<Component> components = _components[entityId];

            components = components.FindAll(component => component.Disabled == false && component.GetType() == typeof(T));

            return components.Cast<T>().ToList();
        }

        protected List<T> GetAllComponentsOfType<T>()
        {
            List<Component> components = new List<Component>();
            _components.Values.ToList().ForEach(components.AddRange);

            return components.Where(component => component is T).Cast<T>().ToList();

        }

        protected List<T> GetAllComponentOfTypeExcept<T>(int entityId) where T : class
        {
            List<T> components = new List<T>();

            foreach (KeyValuePair<int, List<Component>> keyValuePair in _components.Where(keyValuePair => keyValuePair.Key != entityId))
            {
                components.AddRange(GetComponentsByEntityId<T>(keyValuePair.Key));
            }

            return components;
        }
    }
}
