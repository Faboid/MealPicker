using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPickerLibrary.Queries {
    public class ListRecipesResult {
        public int number { get; set; }
        public RecipeModel[] results { get; set; }

    }
}
