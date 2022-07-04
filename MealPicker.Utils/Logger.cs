namespace MealPicker.Utils; 

/// <summary>
/// Implements logging based on a single file.
/// </summary>
public class Logger : ILogger {
    
    /// <summary>
    /// Initializes <see cref="Logger"/> with the default path.
    /// </summary>
    public Logger() : this(Path.Combine(PathBuilder.GetWorkingDirectory, "Logs.txt")) { }

    /// <summary>
    /// Initializes <see cref="Logger"/> with a custom file.
    /// </summary>
    /// <param name="logFilePath"></param>
    public Logger(string logFilePath) {
        logFile = logFilePath;
        Open();
    }

    protected readonly string logFile;
    private const string open = "###START###";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual async Task LogAsync(ILogger.Level level, string message) {
        var msg = Build(level, message);
        await File.AppendAllLinesAsync(logFile, new string[] { msg });
    }

    public virtual Task LogErrorAsync(string message) => LogAsync(ILogger.Level.Error, message);
    public virtual Task LogWarningAsync(string message) => LogAsync(ILogger.Level.Warning, message);
    public virtual Task LogInfoAsync(string message) => LogAsync(ILogger.Level.Info, message);
    public virtual Task LogDebugAsync(string message) => LogAsync(ILogger.Level.Debug, message);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual void Log(ILogger.Level level, string message) {
        var msg = Build(level, message);
        File.AppendAllLines(logFile, new string[] { msg });
    }

    public virtual void LogError(string message) => Log(ILogger.Level.Error, message);
    public virtual void LogWarning(string message) => Log(ILogger.Level.Warning, message);
    public virtual void LogInfo(string message) => Log(ILogger.Level.Info, message);
    public virtual void LogDebug(string message) => Log(ILogger.Level.Debug, message);

    /// <summary>
    /// Formats the message and level in a consistent format.
    /// </summary>
    /// <param name="level"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    protected virtual string Build(ILogger.Level level, string message) => $"{DateTime.Now} - {level}: {message}";

    /// <summary>
    /// Runs on initialization. Adds two lines to log the start of a new session, 
    /// and deletes outdated sessions to keep the log file small.
    /// </summary>
    protected virtual void Open() {
        string openTime = $"###{DateTime.Now}###";
        string[] lines = new string[] { open, openTime };

        File.AppendAllLines(logFile, lines);

        //check if there are many
        //allow up to 10(?) records

        int count = File
            .ReadLines(logFile)
            .Where(x => x == open)
            .Count();
        int max = 10;

        if(count <= max) {
            return;
        }

        bool toKeep = false;
        var linesToKeep = File
            .ReadLines(logFile)
            .Where(x => {
                if(x == open) {
                    count--;
                }

                //since "open" is at the start of the session's log and not at the end,
                //it's needed to only count it once it's finished
                //therefore, instead of <= max, < max is used.
                if(count < max) {
                    toKeep = true;
                }

                return toKeep;
            });

        var tempFile = Path.Combine(PathBuilder.GetWorkingDirectory, "tempdump###ignore.txt");
        File.WriteAllLines(tempFile, linesToKeep);
        File.Delete(logFile);
        File.Move(tempFile, logFile);

    }

}
