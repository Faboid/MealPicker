using MealPicker.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace MealPicker.UI.WPF.ViewModels;

public class RecipeViewModel : ViewModelBase {

	private readonly RecipeModel _recipe;

	public RecipeViewModel(RecipeModel recipe) {
		_recipe = recipe;
	}

	public string Title => _recipe.Title;
	public string Image => _recipe.Image;
	public int ReadyInMinutes => _recipe.ReadyInMinutes;
	public int Servings => _recipe.Servings;
	public IEnumerable<IngredientViewModel> ExtendedIngredients => _recipe.ExtendedIngredients.Select(x => new IngredientViewModel(x));

	public string Summary => _recipe.Summary;
	public string SourceName => _recipe.SourceName;
	public string SourceUrl => _recipe.SourceUrl;

}