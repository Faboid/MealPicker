using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPicker.UI.WPF.Models {
    internal class RecipeModel {

        public RecipeModel(int id, string title, string image, int readyInMinutes, int servings, string sourceName, string sourceUrl, IngredientModel[] extendedIngredients, string summary) {
            Id = id;
            Title = title;
            Image = image;
            ReadyInMinutes = readyInMinutes;
            Servings = servings;
            SourceName = sourceName;
            SourceUrl = sourceUrl;
            ExtendedIngredients = extendedIngredients;
            Summary = summary;
        }

        public RecipeModel(Core.Models.RecipeModel recipe) {
            Id = recipe.Id;
            Title = recipe.Title;
            Image = recipe.Image;
            ReadyInMinutes = recipe.ReadyInMinutes;
            Servings = recipe.Servings;
            SourceName = recipe.SourceName;
            SourceUrl = recipe.SourceUrl;
            ExtendedIngredients = recipe.ExtendedIngredients.Cast<IngredientModel>().ToArray();
            Summary = recipe.Summary;
        }

        public int Id { get; set; }
        public string Title { get; set; }

        //url to the image
        public string Image { get; set; }

        public int ReadyInMinutes { get; set; }
        public int Servings { get; set; }

        //authors name
        public string SourceName { get; set; }
        public string SourceUrl { get; set; }
        public IngredientModel[] ExtendedIngredients { get; set; }
        public string Summary { get; set; }

        public static implicit operator RecipeModel(Core.Models.RecipeModel recipe) => new(recipe);

    }
}
