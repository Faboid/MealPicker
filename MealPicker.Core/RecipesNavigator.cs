using MealPicker.Async;
using MealPicker.Core.Models;
using MealPicker.Core.Services;
using MealPicker.Utils;
using MealPicker.Utils.Options;
using Microsoft.Extensions.Logging;

namespace MealPicker.Core;

/// <summary>
/// Manages requests and validation of recipes.
/// </summary>
public class RecipesNavigator {

    /// <summary>
    /// Creates an instance of <see cref="RecipesNavigator"/> with the given <see cref="ILogger"/> and <see cref="IConnectionService"/>.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="connection"></param>
    public RecipesNavigator(IConnectionService connection, ILoggerFactory? loggerFactory = null) {
        _connection = connection;
        logger = loggerFactory?.CreateLogger<RecipesNavigator>();

        //to respects the Spoonacular API's terms, all cached data needs to be deleted no later than one hour after getting it.
        containerRecipes = new(new List<RecipeModel>(), TimeSpan.FromHours(1), loggerFactory);
    }

    private readonly ILogger<RecipesNavigator>? logger;
    private readonly Locker locker = new(1);
    private readonly TimeoutCollection<RecipeModel> containerRecipes;
    private readonly IConnectionService _connection;

    /// <summary>
    /// Requests the next recipe asynchronously.
    /// </summary>
    /// <returns>An option with <see cref="RecipeModel"/> on success and a <see cref="string"/> error message on failure.</returns>
    public async Task<Option<RecipeModel, string>> NextAsync() {

        using var locked = await locker.GetLockAsync(50);
        if(!locked.Obtained) {
            return Option.None<RecipeModel, string>();
        }

        return await NoLock_NextAsync().ConfigureAwait(false);
    }

    private async Task<Option<RecipeModel, string>> NoLock_NextAsync() {

        while(containerRecipes.Count == 0) {
                    
            var optionRecipes = await _connection.GetRandomRecipesAsync(100);

            var result = optionRecipes.Result();

            if(result is OptionResult.Error or OptionResult.None) {
                var err = optionRecipes.OrError(new(System.Net.HttpStatusCode.BadRequest, "Unknown."));
                string msg = $"The call to get random recipes has failed, code {err.StatusCode}. Reason: {err.ReasonPhrase}";
                logger?.LogError("The call to get random recipes has failed, code {StatusCode}. Reason: {Reason}", err.StatusCode, err.ReasonPhrase);
                return msg;
            }

            var list = optionRecipes
                .Or(new())
                .Recipes
                .Where(x => ValidateValues(x))
                .ToList();

            containerRecipes.Renew(list);

        }

        return containerRecipes.Next().Match(
            some => some,
            () => Option.None<RecipeModel, string>()
        );

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
