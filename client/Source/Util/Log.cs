using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SealSoundMaker.Util
{

    public readonly record struct LogMessage(string content, LogType type, DateTime timestamp)
    {
        public string Format() => $"[{timestamp}] [{type}] {content}";
    }

    public enum LogType
    {
        INFO,
        WARN,
        ERROR,
        DEBUG
    }

    public static class Log
    {
        public static Action<LogMessage> onNewMessage;
        private static ObservableCollection<LogMessage> messages;
        private static bool initialized;

        private static LogMessage recentMessage;

        public static void Init()
        {
            messages = new ObservableCollection<LogMessage>();
            messages.CollectionChanged += (sender, e) => onNewMessage?.Invoke(recentMessage);
            initialized = true;
        }

        private static void Write(string message, LogType type)
        {
            if (!initialized) throw new Exception("Tried to write to log before initialization");

            LogMessage mes = new LogMessage(message, type, DateTime.Now);
            recentMessage = mes;
            messages.Add(mes);
        }

        public static void Info(string message)
        {
            Write(message, LogType.INFO);
        }

        public static void Warn(string message)
        {
            Write(message, LogType.WARN);
        }

        public static void Error(string message)
        {
            Write(message, LogType.ERROR);
        }

        public static void Debug(string message)
        {
            Write(message, LogType.DEBUG);
        }

        public static List<LogMessage> GetMessages()
        {
            return [.. messages];
        }

        public static void Clear() => messages.Clear();
    }
}
