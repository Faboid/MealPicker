namespace MealPicker.Core.Services; 

public class API_Key {

    private readonly string key;
    private const string apiKeySign = "apiKey";

    public API_Key(string key) {
        this.key = key;
    }

    public override string ToString() => key;

    public string GetQueryTestKey() => $"random?{apiKeySign}={key}&number=1&limitLicense=true";
    public string GetQueryRandomRecipes(int recipes) => $"random?{apiKeySign}={key}&number={recipes}&limitLicense=true";
    public string GetQueryRecipeById(string id) => $"{id}/information?{apiKeySign}={key}&includeNutrition=false";

}