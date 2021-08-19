using MealPickerLibrary.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MealPickerLibrary {
    public static class RecipesNavigator {

        public static RecipeModel CurrentRecipe { get; private set; }

        //todo - start a timer that deletes all cached recipes after an hour to follow spoonacular's TOS
        private static List<RecipeModel> recipes = new List<RecipeModel>();

        private static SemaphoreSlim semaphore = new SemaphoreSlim(1);

        public static async Task<bool> Next() {

            if(!semaphore.Wait(50)) {
                return false;
            }

            if(CurrentRecipe is not null) {
                recipes.Remove(CurrentRecipe);
                CurrentRecipe = null;
            }

            if(recipes.Count is 0) {

                //call API to refresh count
                recipes = (await Connection.GetRandomRecipesAsync(150)).Recipes.ToList();

            }

            CurrentRecipe = recipes.First();

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
