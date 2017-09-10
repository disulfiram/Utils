using System;
using System.IO;

namespace Utils.Logger
{
    /// <summary>
    /// Logs messages in a file.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Location of the log file.
        /// </summary>
        private string _logLocation;

        /// <summary>
        /// Indicates wether log file should be overwritten.
        /// </summary>
        private bool _overwrite;

        /// <summary>
        /// Full path to the log file.
        /// </summary>
        private string _logFile;

        /// <summary>
        /// Level of logs that will be written to the log file.
        /// </summary>
        private LoggingLevel _loggingLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="logLocation">Location of the log file.</param>
        /// <param name="loggingLevel">Level of logs that will be written to the log file. <seealso cref="LoggingLevel"/></param>
        /// <param name="overwrite">If set to false will create new log file each time the program is run.</param>
        public Logger(string logLocation, LoggingLevel loggingLevel, bool overwrite)
        {
            this._logLocation = logLocation;
            this._loggingLevel = loggingLevel;
            this._overwrite = overwrite;

            if (overwrite)
            {
                this._logFile = Path.Combine(this._logLocation, "log.txt");
                if (File.Exists(this._logFile))
                    File.Delete(this._logFile);
            }
            else
                this._logFile = Path.Combine(this._logLocation, string.Format("log-{0}.txt", DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")));
        }

        /// <summary>
        /// Writes error message to the log. Significantly more verbose than <see cref="Error(string)"/> method.
        /// </summary>
        /// <param name="ex">Exception to be logged.</param>
        public void Exception(Exception ex)
        {
            if (this._loggingLevel >= LoggingLevel.Errors)
            {
                using (var file = File.AppendText(this._logFile))
                {
                    file.WriteLine(FormatMessage(ex.Message, MessageType.Error));
                    file.WriteLine(ex.StackTrace);
                }

                if (ex.InnerException != null)
                {
                    this.Exception(ex.InnerException);
                }
            }
        }

        /// <summary>
        /// Writes error message to the log.
        /// </summary>
        /// <param name="message">Message that will be written.</param>
        public void Error(string message)
        {
            if (this._loggingLevel >= LoggingLevel.Errors)
                using (var file = File.AppendText(this._logFile))
                {
                    file.WriteLine(FormatMessage(message, MessageType.Error));
                }
        }

        /// <summary>
        /// Writes error messages to the log.
        /// </summary>
        /// <param name="messages">Messages that will be written.</param>
        public void Error(params string[] messages)
        {
            foreach (var message in messages)
                this.Error(message);
        }

        /// <summary>
        /// Writes warning message to the log.
        /// </summary>
        /// <param name="message">Message that will be written.</param>
        public void Warning(string message)
        {
            if (this._loggingLevel >= LoggingLevel.Warning)
                using (var file = File.AppendText(this._logFile))
                {
                    file.WriteLine(FormatMessage(message, MessageType.Warning));
                }
        }

        /// <summary>
        /// Writes warning messages to the log.
        /// </summary>
        /// <param name="messages">Messages that will be written.</param>
        public void Warning(params string[] messages)
        {
            foreach (var message in messages)
                this.Warning(message);
        }

        /// <summary>
        /// Writes important message to the log.
        /// </summary>
        /// <param name="message">Message that will be written.</param>
        public void ImportantMessage(string message)
        {
            if (this._loggingLevel >= LoggingLevel.ImportantMessages)
            {
                using (var file = File.AppendText(this._logFile))
                {
                    file.WriteLine(FormatMessage(message, MessageType.ImportantMessage));
                }
            }
        }

        /// <summary>
        /// Writes important messages to the log.
        /// </summary>
        /// <param name="messages">Messages that will be written.</param>
        public void ImportantMessage(params string[] messages)
        {
            foreach (var message in messages)
                this.ImportantMessage(message);
        }

        /// <summary>
        /// Writes info message to the log.
        /// </summary>
        /// <param name="message">Message that will be written.</param>
        public void Info(string message)
        {
            if (this._loggingLevel >= LoggingLevel.All)
            {
                using (var file = File.AppendText(this._logFile))
                {
                    file.WriteLine(FormatMessage(message, MessageType.Info));
                }
            }
        }

        /// <summary>
        /// Writes info messages to the log.
        /// </summary>
        /// <param name="messages">Messages that will be written.</param>
        public void Info(params string[] messages)
        {
            foreach (var message in messages)
                this.Info(message);
        }

        /// <summary>
        /// Formats message that will be logged.
        /// </summary>
        /// <param name="message">Message that will be logged.</param>
        /// <param name="type">Message type.</param>
        /// <returns>Formatted log message.</returns>
        private static string FormatMessage(string message, MessageType type)
        {
            return $"{DateTime.Now}: [{type}] - {message}";
        }
    }
}
