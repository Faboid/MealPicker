namespace MealPicker.Utils; 

public class Settings {

    public Settings() : this(PathBuilder.SettingsPath) { }

    internal Settings(string path) {
        filePath = path;

        if(!File.Exists(path)) {
            File.Create(path).Dispose();
        }
    }

    private readonly string filePath;
    private const string separator = ": ";
    //key: value

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

    public Option<string> Get(string key) {
        return File.ReadAllLines(filePath)
            .Select(x => x.Split(separator))
            .Where(x => x[0] == key)
            .Select<string[], Option<string>>(x => x[1])
            .FirstOrDefault(Option.None<string>());
    }

}
