using MealPickerLibrary.Queries;
using MealPickerLibrary.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MealPickerLibrary {
    public static class RecipesNavigator {

        public static RecipeModel CurrentRecipe { get; private set; }

        private static TimeoutContainer<RecipeModel> containerRecipes = new TimeoutContainer<RecipeModel>(3600000); //3600000 = 1 hour

        private static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        public static async Task<bool> Next() {

            if(!semaphore.Wait(50)) {
                return false;
            }

            if(CurrentRecipe is not null) {
                containerRecipes.Content.Remove(CurrentRecipe);
                CurrentRecipe = null;
            }

            if(containerRecipes.Content.Count is 0) {

                //call API to refresh count
                var recipes = (await Connection.GetRandomRecipesAsync(150)).Recipes.ToList();
                containerRecipes.SetContent(recipes);
            }

            CurrentRecipe = containerRecipes.Content.First();

            semaphore.Release();

            if(ValidateValues() is false) {
                await Next();
            }

            return true;
        }

        private static bool ValidateValues() {
            bool[] conditions = new bool[] {
                CurrentRecipe.Id is not null,
                CurrentRecipe.Title is not null,
                CurrentRecipe.Image is not null,
                CurrentRecipe.ReadyInMinutes is not 0,
                CurrentRecipe.Servings is not 0,
                CurrentRecipe.SourceName is not null,
                CurrentRecipe.SourceUrl is not null,
                CurrentRecipe.extendedIngredients is not null,
                CurrentRecipe.Summary is not null
            };

            return conditions.All(x => x is true);
        }

    }
}
