namespace MealPicker.Core.Models; 

public class RecipeModel {

    /// <summary>
    /// The ID of the recipe.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The title of the recipe.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// The URL to get the image.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// Total minutes required to complete the recipe.
    /// </summary>
    public int ReadyInMinutes { get; set; }

    /// <summary>
    /// Number of servings.
    /// </summary>
    public int Servings { get; set; }

    /// <summary>
    /// Author's name.
    /// </summary>
    public string SourceName { get; set; }

    /// <summary>
    /// URL to the recipe.
    /// </summary>
    public string SourceUrl { get; set; }

    /// <summary>
    /// A list with all ingredients.
    /// </summary>
    public IngredientModel[] ExtendedIngredients { get; set; }

    /// <summary>
    /// The summary of the recipe.
    /// </summary>
    public string Summary { get; set; }

}
