using System.Linq;

namespace MealPicker.UI.WPF.Models {

    /// <summary>
    /// Represents a recipe.
    /// </summary>
    public class RecipeModel {

        public RecipeModel(int id, string title, string image, int readyInMinutes, int servings, string sourceName, string sourceUrl, IngredientModel[] extendedIngredients, string summary) {
            Id = id;
            Title = title;
            Image = image;
            ReadyInMinutes = readyInMinutes;
            Servings = servings;
            SourceName = sourceName;
            SourceUrl = sourceUrl;
            ExtendedIngredients = extendedIngredients;
            Summary = Core.Conversions.HTML.ToPlainText(summary);
        }

        public RecipeModel(Core.Models.RecipeModel recipe) {
            Id = recipe.Id;
            Title = recipe.Title;
            Image = recipe.Image;
            ReadyInMinutes = recipe.ReadyInMinutes;
            Servings = recipe.Servings;
            SourceName = recipe.SourceName;
            SourceUrl = recipe.SourceUrl;
            ExtendedIngredients = recipe.ExtendedIngredients.Select(x => (IngredientModel)x).ToArray();
            Summary = Core.Conversions.HTML.ToPlainText(recipe.Summary);
        }

        /// <summary>
        /// The ID of the recipe.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the recipe.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The URL to get the image.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Total minutes required to complete the recipe.
        /// </summary>
        public int ReadyInMinutes { get; set; }

        /// <summary>
        /// Number of servings.
        /// </summary>
        public int Servings { get; set; }

        /// <summary>
        /// Author's name.
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// URL to the recipe.
        /// </summary>
        public string SourceUrl { get; set; }

        /// <summary>
        /// A list with all ingredients.
        /// </summary>
        public IngredientModel[] ExtendedIngredients { get; set; }

        /// <summary>
        /// The summary of the recipe.
        /// </summary>
        public string Summary { get; set; }

        public static implicit operator RecipeModel(Core.Models.RecipeModel recipe) => new(recipe);

    }
}
