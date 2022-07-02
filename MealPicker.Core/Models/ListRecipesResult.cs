namespace MealPicker.Core.Models;

/// <summary>
/// A wrapper for an array of <see cref="RecipeModel"/>.
/// </summary>
public class ListRecipesResult {

    /// <summary>
    /// A list of <see cref="RecipeModel"/>.
    /// </summary>
    public RecipeModel[] Recipes { get; set; }

}
