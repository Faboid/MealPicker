using MealPickerLibrary.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPickerLibrary {
    public static class RecipesNavigator {

        public static RecipeModel CurrentRecipe { get; private set; }

        private static List<RecipeModel> recipes = new List<RecipeModel>();

        public static async Task<bool> Next() {
            if(CurrentRecipe is not null) {
                recipes.Remove(CurrentRecipe);
                CurrentRecipe = null;
            }

            if(recipes.Count is 0) {
                //call API to refresh count
                recipes = (await Connection.GetRandomRecipesAsync(150)).Recipes.ToList();
            }

            CurrentRecipe = recipes.First();

            return true;
        }

        private static void Refresh() {

        }

    }
}
