namespace MealPicker.UI.WPF.Models;

/// <summary>
/// A model for a single ingredient.
/// </summary>
public class IngredientModel {

    /// <summary>
    /// Initializes <see cref="IngredientModel"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="amount"></param>
    /// <param name="unit"></param>
    public IngredientModel(string name, double amount, string unit) {
        Name = name;
        Amount = amount;
        Unit = unit;
    }

    /// <summary>
    /// Initializes <see cref="IngredientModel"/>.
    /// </summary>
    /// <param name="ingredient"></param>
    public IngredientModel(Core.Models.IngredientModel ingredient) {
        Name = ingredient.Name;
        Amount = ingredient.Amount;
        Unit = ingredient.Unit;
    }

    /// <summary>
    /// The name of the ingredient.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The amount of this ingredient used by the recipe.
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    /// The unit used to measure the <see cref="Amount"/>.
    /// </summary>
    public string Unit { get; set; }

    public static implicit operator IngredientModel(Core.Models.IngredientModel ingredient) => new(ingredient);

}
