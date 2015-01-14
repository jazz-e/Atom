using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Atom.Exceptions;
using Atom.Logging.Loggers;

namespace Atom.Logging
{
    public class LogFactory
    {
        private static LogFactory _instance = new LogFactory();

        private Dictionary<string, Type> _loggers = new Dictionary<string, Type>();

        private LogFactory()
        {
            Register<ConsoleLogger>("Console");
        }

        /// <summary>
        /// Registers the provided logger type to the factory. It will store it against the id given. Use this id to retrieve an instance of the logger
        /// </summary>
        /// <param name="id">Unique identifier to store the logger against</param>
        /// <param name="type">The logger type to be registered</param>
        public void Register(string id, Type type)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            if (type == null)
                throw new ArgumentNullException("type");

            if (_loggers.ContainsKey(id))
                throw new IdAlreadyExists();

            if (!typeof(ILogger).IsAssignableFrom(type))
                throw new NotILoggerException();

            _loggers.Add(id, type);
        }

        /// <summary>
        /// Registers a new logger to the factory. It will store it against the name of the type.
        /// </summary>
        /// <typeparam name="T">The logger you want to register</typeparam>
        /// <returns>The id that the logger has been stored against</returns>
        public string Register<T>() where T : class, ILogger
        {
            Type loggerType = typeof (T); 
            
            Register(loggerType.Name, loggerType);

            return loggerType.Name;
        }

        /// <summary>
        /// Registers a new logger type to the factory. It will store it against the id given.
        /// </summary>
        /// <typeparam name="T">The logger type you want to register</typeparam>
        /// <param name="id">The id to store the logger against</param>
        public void Register<T>(string id) where T : class, ILogger
        {
            Type loggerType = typeof(T);

            Register(id, loggerType);
        }

        /// <summary>
        /// Constructs a logger from the types that have been registered
        /// </summary>
        /// <param name="id">The id of the Logger to construct</param>
        /// <returns>Returns a new logger from the id given. Will return null if logger failed to construct</returns>
        public ILogger Construct(string id)
        {
            Type loggerType = _loggers[id];

            if (loggerType == null)
                return null;

            var logger = Activator.CreateInstance(loggerType) as ILogger;

            return logger;
        }

        /// <summary>
        /// Returns the instance of the LogFactory so you can register and construct loggers
        /// </summary>
        /// <returns>Returns a LogFactory</returns>
        public static LogFactory GetInstance()
        {
            return _instance;
        }
    }
}
