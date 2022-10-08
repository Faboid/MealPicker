using System.Windows.Input;

namespace MealPicker.UI.WPF.ViewModels;

public class RecipeGeneratorViewModel : ViewModelBase {

	private RecipeViewModel? _recipeViewModel = null;
	public RecipeViewModel? RecipeViewModel {
		get { return _recipeViewModel; }
		set { _recipeViewModel = value; }
	}

	public ICommand GenerateRandomRecipeCommand { get; }

	public RecipeGeneratorViewModel() {

	}

}