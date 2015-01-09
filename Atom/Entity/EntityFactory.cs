using System;
using System.Collections.Generic;
using System.Linq;

namespace Atom.Entity
{
    public class EntityFactory
    {
        private static EntityFactory _instance = new EntityFactory();

        private HashSet<Type> _entityTypes = new HashSet<Type>();

        private int _currentEntityId = 0;

        private EntityFactory() {}

        /// <summary>
        /// Registers an entity type that can be used to create an instance of that entity
        /// </summary>
        /// <param name="type"></param>
        public void RegisterEntity(Type type)
        {
            if (!type.IsAssignableFrom(typeof (Entity)))
            {
                throw new NotEntityException();
            }

            _entityTypes.Add(type);
        }

        /// <summary>
        /// Creates a entity from the type given. The type needs to be registered before it can be created.
        /// </summary>
        /// <typeparam name="T">The type of entity that will be created</typeparam>
        /// <returns>Returns a new instance of the Entity</returns>
        public T CreateEntity<T>() where T : Entity
        {
            IEnumerable<Type> types = _entityTypes.Where(type => type.FullName == typeof (T).FullName);

            var enumerable = types as IList<Type> ?? types.ToList();

            if (!enumerable.Any())
            {
                return null;
            }

            var entity = Activator.CreateInstance(enumerable.First()) as Entity;

            if (entity == null)
            {
                return null;
            }

            entity.Id = GetNextEntityId();

            return entity as T;
        }

        /// <summary>
        /// Gets the next avaliable entity id
        /// </summary>
        /// <returns></returns>
        public int GetNextEntityId()
        {
            return ++_currentEntityId;
        }

        /// <summary>
        /// Gets the instance of the EntityFactory
        /// </summary>
        /// <returns></returns>
        public static EntityFactory GetInstance()
        {
            return _instance;
        }
    }
}
