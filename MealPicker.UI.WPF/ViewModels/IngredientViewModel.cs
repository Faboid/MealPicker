using MealPicker.Core.Models;

namespace MealPicker.UI.WPF.ViewModels;

public class IngredientViewModel : ViewModelBase {

	private readonly IngredientModel _ingredient;

	public IngredientViewModel(IngredientModel ingredient) {
		_ingredient = ingredient;
	}

	public string Name => _ingredient.Name;
	public float Amount => _ingredient.Amount;
	public string Unit => _ingredient.Unit;

}