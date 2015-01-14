﻿namespace Atom.Logging
{
    public interface ILogger
    {
        void Log(string message);
        void Log(LogLevel level, string message);
        void Log(LogLevel level, string message, params object[] args);
    }
}
