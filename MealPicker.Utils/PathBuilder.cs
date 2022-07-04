namespace MealPicker.Utils;

/// <summary>
/// Provides static paths to files.
/// </summary>
public static class PathBuilder {

    /// <summary>
    /// Gets the directory where the application's exe has been executed in.
    /// </summary>
    public static string GetWorkingDirectory { get; } = Path.GetDirectoryName(Environment.ProcessPath)!;

    /// <summary>
    /// Gets the path to the API Key.
    /// </summary>
    public static string GetKeyPath { get; } = Path.Combine(GetWorkingDirectory, "Key.txt");

    /// <summary>
    /// Gets the path to the settings file.
    /// </summary>
    public static string SettingsPath { get; } = Path.Combine(GetWorkingDirectory, "Settings.txt");
}
