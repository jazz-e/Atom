using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Atom
{
    public static class GameServices
    {
        private static GameServiceContainer _container;

        /// <summary>
        /// Gets the ContentManager
        /// </summary>
        public static ContentManager Content
        {
            get { return GetService<ContentManager>(); }
        }

        /// <summary>
        /// Gets the GraphicsDeviceManager
        /// </summary>
        public static GraphicsDeviceManager Graphics
        {
            get { return GetService<GraphicsDeviceManager>(); }
        }

        /// <summary>
        /// The instance of GameServiceContainer
        /// </summary>
        public static GameServiceContainer Instance
        {
            get { return _container ?? (_container = new GameServiceContainer()); }
        }

        /// <summary>
        /// Initialise the container and add the basic service that you might want everywhere.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphics"></param>
        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            _container = new GameServiceContainer();
            AddService(content);
            AddService(graphics);
        }

        /// <summary>
        /// Gets the service for the given type.
        /// </summary>
        /// <typeparam name="T">The type of service that you want.</typeparam>
        /// <returns>Returns your service or null.</returns>
        public static T GetService<T>()
        {
            return (T) Instance.GetService(typeof (T));
        }

        /// <summary>
        /// Adds the service to a container, so that it can be accessed anywhere.
        /// </summary>
        /// <typeparam name="T">Optional param to tell the type of service you are adding.</typeparam>
        /// <param name="service">The service you want to access anywhere.</param>
        public static void AddService<T>(T service)
        {
            Instance.AddService(typeof(T), service);
        }

        /// <summary>
        /// Removes the service from the container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RemoveService<T>()
        {
            Instance.RemoveService(typeof (T));
        }
    }
}
