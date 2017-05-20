namespace Utils.Logger
{
    /// <summary>
    /// Determins types of messages that will be written in the log file.
    /// </summary>
    internal enum LoggingLevel
    {
        /// <summary>
        /// Nothing will be written to the log file.
        /// </summary>
        None,

        /// <summary>
        /// Only Errors will be written to the log file.
        /// </summary>
        Errors,

        /// <summary>
        /// Only Errors and Warning will be written to the log file.
        /// </summary>
        Warning,

        /// <summary>
        /// Only Errors, Warnings and Important Messages will be written to the log file.
        /// </summary>
        ImportantMessages,

        /// <summary>
        /// All log entries will be written to the log file.
        /// </summary>
        All,
    }
}
