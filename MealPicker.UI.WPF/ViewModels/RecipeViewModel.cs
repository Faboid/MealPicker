using MealPicker.UI.WPF.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace MealPicker.UI.WPF.ViewModels;

public class RecipeViewModel : ViewModelBase {

	private RecipeModel? _recipe;
	public RecipeModel? Recipe {
		get => _recipe;
		set {
            if(value == null) {
                return;
            }

            SetAndRaise(nameof(Recipe), ref _recipe, value);
			NotifyAll(value);
		}
	}

	private readonly ObservableCollection<IngredientViewModel> _ingredients = new();

	public string Title => _recipe?.Title ?? "Hello!";
	public string Image => _recipe?.Image ?? "";
	public int ReadyInMinutes => _recipe?.ReadyInMinutes ?? 0;
	public int Servings => _recipe?.Servings ?? 0;
	public IEnumerable<IngredientViewModel> ExtendedIngredients => _ingredients;
	public string Summary => _recipe?.Summary ?? "";
	public string SourceName => _recipe?.SourceName	?? "";
	public string SourceUrl => _recipe?.SourceUrl ?? "";

	private void NotifyAll(RecipeModel recipe) {
		OnPropertyChanged(nameof(Title));
		OnPropertyChanged(nameof(Image));
		OnPropertyChanged(nameof(ReadyInMinutes));
		OnPropertyChanged(nameof(Servings));
		OnPropertyChanged(nameof(ExtendedIngredients));
		OnPropertyChanged(nameof(Summary));
		OnPropertyChanged(nameof(SourceName));
		OnPropertyChanged(nameof(SourceUrl));
		LoadIngredients(recipe.ExtendedIngredients);
	}

	private void LoadIngredients(IEnumerable<IngredientModel> ingredients) {
		_ingredients.Clear();
		foreach(var ingredient in ingredients) {
			_ingredients.Add(new(ingredient));
		}
		OnPropertyChanged(nameof(ReadyInMinutes));
	}

}