namespace MealPicker.Utils; 

/// <summary>
/// Provides an easy way of saving key-value string pairs throughout sessions.
/// </summary>
public class Settings {

    /// <summary>
    /// Initializes <see cref="Settings"/> by saving in the default path.
    /// </summary>
    public Settings() : this(PathBuilder.SettingsPath) { }

    /// <summary>
    /// Initializes <see cref="Settings"/> by saving in a custom path.
    /// </summary>
    /// <param name="path"></param>
    internal Settings(string path) {
        filePath = path;

        if(!File.Exists(path)) {
            File.Create(path).Dispose();
        }
    }

    private readonly string filePath;
    private const string separator = ": ";
    //key: value

    /// <summary>
    /// Creates or updates the given <paramref name="key"/> with the given <paramref name="value"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Set(string key, string value) {
        string[] def = new string[] { $"not{key}", "" };
        
        var lines = File.ReadAllLines(filePath)
            .Select(x => x.Split(separator))
            .ToList();

        var line = lines
            .Where(x => x[0] == key)
            .FirstOrDefault(def);

        if(line != def) {
            line[1] = value;
        } else {
            lines.Add(new string[] {key, value });
        }

        File.WriteAllLines(filePath, lines.Select(x => $"{x[0]}{separator}{x[1]}"));
    }

    /// <summary>
    /// Returns <see cref="Option{TValue}"/>.Some with a string value if it exists, otherwise, it returns <see cref="Option.None{TValue}"/>.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public Option<string> Get(string key) {
        return File.ReadAllLines(filePath)
            .Select(x => x.Split(separator))
            .Where(x => x[0] == key)
            .Select<string[], Option<string>>(x => x[1])
            .FirstOrDefault(Option.None<string>());
    }

}
