using MealPicker.Core;
using MealPicker.UI.WPF.Models;
using MealPicker.Utils;
using System.Threading.Tasks;

namespace MealPicker.UI.WPF.Stores;

public class RecipeStore {

    private readonly RecipesNavigator _recipesNavigator;

    public RecipeStore(RecipesNavigator recipesNavigator) {
        _recipesNavigator = recipesNavigator;
    }


    public async Task<Option<RecipeModel, string>> GetNextRandomRecipeAsync() {
        return (await _recipesNavigator.NextAsync())
            .Bind<RecipeModel>(x => new(x));
    }

}