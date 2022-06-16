namespace MealPicker.UI.WPF.Models; 

internal class IngredientModel {

    public IngredientModel(string name, double amount, string unit) {
        Name = name;
        Amount = amount;
        Unit = unit;
    }

    public IngredientModel(Core.Models.IngredientModel ingredient) {
        Name = ingredient.Name;
        Amount = ingredient.Amount;
        Unit = ingredient.Unit;
    }

    public string Name { get; set; }
    public double Amount { get; set; }
    public string Unit { get; set; }

    public static implicit operator IngredientModel(Core.Models.IngredientModel ingredient) => new(ingredient);

}
