namespace MealPicker.Core.Models;

/// <summary>
/// A model for a single ingredient.
/// </summary>
public class IngredientModel {

    /// <summary>
    /// The name of the ingredient.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The amount of this ingredient used by the recipe.
    /// </summary>
    public float Amount { get; set; }

    /// <summary>
    /// The unit used to measure the <see cref="Amount"/>.
    /// </summary>
    public string Unit { get; set; }


}
