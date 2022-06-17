using MealPicker.Async;
using MealPicker.Core.Models;
using MealPicker.Core.Services;
using MealPicker.Utils;

namespace MealPicker.Core;

public class RecipesNavigator {

    public RecipesNavigator(IConnectionService connection) {
        this.connection = connection;
    }

    private readonly Locker locker = new(1);
    private readonly TimeoutCollection<RecipeModel> containerRecipes = new(new(), TimeSpan.FromHours(1));
    private readonly IConnectionService connection;

    public async Task<Option<RecipeModel>> NextAsync() {

        using var locked = await locker.GetLockAsync(50);
        if(!locked.Obtained) {
            return Option.None<RecipeModel>();
        }

        return await NoLock_NextAsync().ConfigureAwait(false);
    }

    //todo - decide what to do if all the results that get returned are faulted(infinite loop)
    private async Task<Option<RecipeModel>> NoLock_NextAsync() {

        if(containerRecipes.Count == 0) {
            var recipes = await connection.GetRandomRecipesAsync(100);
            containerRecipes.Renew(recipes.Or(new()).Recipes.ToList());
        }

        var recipe = containerRecipes.Next();

        var result = recipe.Bind<bool>(x => ValidateValues(x)).Or(false);
        if(!result) {
            return await NoLock_NextAsync();
        }

        return recipe;
    }

    private static bool ValidateValues(RecipeModel recipe) {
        if(recipe is null) {
            return false;
        }
        
        bool[] conditions = new bool[] {
            recipe.Title is not null,
            recipe.Image is not null,
            recipe.ReadyInMinutes is not 0,
            recipe.Servings is not 0,
            recipe.SourceName is not null,
            recipe.SourceUrl is not null,
            recipe.ExtendedIngredients is not null,
            recipe.Summary is not null
        };

        return conditions.All(x => x is true);
    }

}
