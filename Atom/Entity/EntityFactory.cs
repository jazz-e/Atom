using System;
using System.Collections.Generic;
using System.Linq;

namespace Atom.Entity
{
    public class EntityFactory
    {
        private static EntityFactory _instance = new EntityFactory();

        private Dictionary<string, Type> _entityTypes = new Dictionary<string, Type>();

        private int _currentEntityId = 0;

        private EntityFactory() {}

        /// <summary>
        /// Registers an entity type that can be used to create an instance of that entity
        /// </summary>
        /// <param name="id">The id that you want to set the entity to</param>
        /// <param name="type">The type of entity</param>
        public void RegisterEntity(string id, Type type)
        {
            if (!type.IsSubclassOf(typeof (Entity)))
            {
                throw new NotEntityException();
            }

            if (_entityTypes.ContainsKey(id))
            {
                return;
            }

            _entityTypes.Add(id, type);
        }

        /// <summary>
        /// Registers an entity type that can be used to create an instance of that entity. It will create an id from the FullName of the type.
        /// </summary>
        /// <param name="type"></param>
        public void RegisterEntity(Type type)
        {
            RegisterEntity(type.FullName, type);
        }

        /// <summary>
        /// Registers an entity type that can be used to create an instance of that entity
        /// </summary>
        /// <typeparam name="T">The type of entity you want to register</typeparam>
        /// <param name="id">The id to give the entity type you are registering</param>
        public void RegisterEntity<T>(string id)
        {
            RegisterEntity(id, typeof (T));
        }

        /// <summary>
        /// Registers an entity type that can be used to create an instance of that entity. It will create an id from the FullName of the type.
        /// </summary>
        /// <typeparam name="T">The type of entity you want to register. Will use type FullName as the id.</typeparam>
        public void RegisterEntity<T>()
        {
            RegisterEntity<T>(typeof (T).FullName);
        }

        /// <summary>
        /// Creates a entity from the type given. The type needs to be registered before it can be created.
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <returns>Returns a new instance of the Entity</returns>
        public Entity CreateEntity(string id)
        {
            Type entityType = _entityTypes[id];

            var entity = Activator.CreateInstance(entityType) as Entity;

            if (entity == null)
            {
                return null;
            }

            entity.Id = GetNextEntityId();

            return entity;
        }

        /// <summary>
        /// Creates a entity from the type given. The type needs to be registered before it can be created.
        /// </summary>
        /// <param name="type">The type of entity you want. Will use FullName as id.</param>
        /// <returns>Returns a new instance of the Entity or null if it fails to do so</returns>
        public Entity CreateEntity(Type type)
        {
            return CreateEntity(type.FullName);
        }

        /// <summary>
        /// Creates a entity from the type given. The type needs to be registered before it can be created.
        /// </summary>
        /// <typeparam name="T">The type you want the Entity to be returned as</typeparam>
        /// <param name="id">The id of the entity</param>
        /// <returns>Returns a new instance of the Entity or null if it fails to do so</returns>
        public T CreateEntity<T>(string id) where T : Entity
        {
            var entity = CreateEntity(id);

            return (T) entity;
        }

        /// <summary>
        /// Creates a entity from the type given. The type needs to be registered before it can be created.
        /// </summary>
        /// <typeparam name="T">The type you want the Entity to be returned as</typeparam>
        /// <returns>Returns a new instance of the Entity or null if it fails to do so</returns>
        public T CreateEntity<T>() where T : Entity
        {
            return CreateEntity<T>(typeof (T).FullName);
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
