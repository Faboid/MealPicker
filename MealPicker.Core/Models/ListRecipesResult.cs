using Newtonsoft.Json;

namespace MealPicker.Core.Models;

public class ListRecipesResult {

    [JsonProperty("recipes")]
    public RecipeModel[] Recipes { get; set; }

}
