namespace MealPicker.Utils; 

/// <summary>
/// An interface with methods to log data.
/// </summary>
public interface ILogger {

    /// <summary>
    /// The level of logging.
    /// </summary>
    public enum Level {
        Debug,
        Info,
        Warning,
        Error,
    }

    /// <summary>
    /// Logs the given message to a file, using the given <see cref="ILogger.Level"/>.
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task LogAsync(Level level, string message);

    /// <summary>
    /// Logs the given message using <see cref="Level.Error"/>.
    /// </summary>
    Task LogErrorAsync(string message);
    /// <summary>
    /// Logs the given message using <see cref="Level.Warning"/>.
    /// </summary>
    Task LogWarningAsync(string message);
    /// <summary>
    /// Logs the given message using <see cref="Level.Info"/>.
    /// </summary>
    Task LogInfoAsync(string message);
    /// <summary>
    /// Logs the given message using <see cref="Level.Debug"/>.
    /// </summary>
    Task LogDebugAsync(string message);

    /// <summary>
    /// Logs the given message to a file, using the given <see cref="ILogger.Level"/>.
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    void Log(Level level, string message);

    /// <summary>
    /// Logs the given message using <see cref="Level.Error"/>.
    /// </summary>
    void LogError(string message);
    /// <summary>
    /// Logs the given message using <see cref="Level.Warning"/>.
    /// </summary>
    void LogWarning(string message);
    /// <summary>
    /// Logs the given message using <see cref="Level.Info"/>.
    /// </summary>
    void LogInfo(string message);
    /// <summary>
    /// Logs the given message using <see cref="Level.Debug"/>.
    /// </summary>
    void LogDebug(string message);

}
