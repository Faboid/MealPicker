using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MealPickerLibrary.Queries {
    public class RecipeModel {


        public string Id { get; set; }
        public string Title { get; set; }

        //url to the image
        public string Image { get; set; }

        public int ReadyInMinutes { get; set; }
        public int Servings { get; set; }

        //authors name
        public string SourceName { get; set; }
        public string SourceUrl { get; set; }
        public IngredientModel[] extendedIngredients { get; set; }
        public string Summary { get; set; }

    }
}
