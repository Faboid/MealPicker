namespace MealPicker.Core.Files;

public static class PathBuilder {

    /// <summary>
    /// Gets the directory where the application's exe has been executed in.
    /// </summary>
    public static readonly string GetWorkingDirectory = Path.GetDirectoryName(Environment.ProcessPath)!;

    /// <summary>
    /// Gets the path to the API Key.
    /// </summary>
    public static readonly string GetKeyPath = Path.Combine(GetWorkingDirectory, "Key.txt");

}
