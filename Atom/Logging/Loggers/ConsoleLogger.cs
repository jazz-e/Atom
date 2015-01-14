using System;
using Microsoft.Xna.Framework;

namespace Atom.Logging.Loggers
{
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Prints a message to the console.
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            Log(LogLevel.Info, message, null);
        }

        /// <summary>
        /// Prints a message to the console but with the given LogLevel
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        public void Log(LogLevel level, string message)
        {
            Log(level, message, null);
        }

        /// <summary>
        /// Prints a message to the console with the given LogLevel
        /// </summary>
        /// <param name="level">The LogLevel at which to display the message</param>
        /// <param name="message">The message to print to the console</param>
        /// <param name="args">Any arguments with the message</param>
        public void Log(LogLevel level, string message, params object[] args)
        {
            Console.WriteLine(level + ": " + message, args);
        }
    }
}
