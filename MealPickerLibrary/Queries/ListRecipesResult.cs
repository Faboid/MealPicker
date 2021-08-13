using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPickerLibrary.Queries {
    public class ListRecipesResult {

        [JsonProperty("recipes")]
        public RecipeModel[] Recipes { get; set; }

    }
}
