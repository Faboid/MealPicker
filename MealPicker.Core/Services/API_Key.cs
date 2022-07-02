namespace MealPicker.Core.Services; 

/// <summary>
/// Contains the key and builds standard queries on top of it.
/// </summary>
public class API_Key {

    private readonly string key;
    private const string apiKeySign = "apiKey";

    /// <summary>
    /// Creates an instance of <see cref="API_Key"/> with the given <paramref name="key"/>.
    /// </summary>
    /// <param name="key"></param>
    public API_Key(string key) {
        this.key = key;
    }

    /// <summary>
    /// Returns the key.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => key;

    /// <summary>
    /// Gets a query that can be used to check the validity of the key.
    /// </summary>
    /// <returns></returns>
    public string GetQueryTestKey() => $"random?{apiKeySign}={key}&number=1&limitLicense=true";

    /// <summary>
    /// Builds a query to request <paramref name="recipes"/> amount of random recipes.
    /// </summary>
    /// <param name="recipes"></param>
    /// <returns></returns>
    public string GetQueryRandomRecipes(int recipes) => $"random?{apiKeySign}={key}&number={recipes}&limitLicense=true";

    /// <summary>
    /// Builds a request to return a specific recipe.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetQueryRecipeById(string id) => $"{id}/information?{apiKeySign}={key}&includeNutrition=false";

}