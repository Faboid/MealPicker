namespace MealPicker.Utils; 

public interface ILogger {

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
    Task LogErrorAsync(string message);
    Task LogWarningAsync(string message);
    Task LogInfoAsync(string message);

    /// <summary>
    /// Logs the given message to a file, using the given <see cref="ILogger.Level"/>.
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    void Log(Level level, string message);
    void LogError(string message);
    void LogWarning(string message);
    void LogInfo(string message);

}
