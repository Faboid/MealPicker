using MealPicker.Async;
using MealPicker.Core.Models;
using MealPicker.Core.Services;
using MealPicker.Utils;
using MealPicker.Utils.Options;

namespace MealPicker.Core;

public class RecipesNavigator {

    public RecipesNavigator(ILogger logger, IConnectionService connection) {
        this.connection = connection;
        this.logger = logger;
        containerRecipes = new(logger, new List<RecipeModel>(), TimeSpan.FromHours(1));
    }

    private readonly ILogger logger;
    private readonly Locker locker = new(1);
    private readonly TimeoutCollection<RecipeModel> containerRecipes;
    private readonly IConnectionService connection;

    public async Task<Option<RecipeModel>> NextAsync() {

        using var locked = await locker.GetLockAsync(50);
        if(!locked.Obtained) {
            return Option.None<RecipeModel>();
        }

        return await NoLock_NextAsync().ConfigureAwait(false);
    }

    private async Task<Option<RecipeModel>> NoLock_NextAsync() {

        while(containerRecipes.Count == 0) {
                    
            var optionRecipes = await connection.GetRandomRecipesAsync(100);

            var result = optionRecipes.Result();

            if(result == OptionResult.Error || result == OptionResult.None) {
                var err = optionRecipes.OrError(new(System.Net.HttpStatusCode.BadRequest, "Unknown."));
                logger.LogError($"The call to get random recipes has failed, code {err.StatusCode}. Reason: {err.ReasonPhrase}");
                continue; //todo - return none or some kind of error
            }

            var list = optionRecipes
                .Or(new())
                .Recipes
                .Where(x => ValidateValues(x))
                .ToList();

            containerRecipes.Renew(list);

        }

        return containerRecipes.Next();

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
