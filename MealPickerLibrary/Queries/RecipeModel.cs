using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealPickerLibrary.Queries {
    public class RecipeModel {


        public string Id { get; set; }
        public string Title { get; set; }

        //url to the image
        public string Image { get; set; }


        //extra info—requires calling the api for the specific recipe.
        public int ReadyInMinutes { get; set; }
        public string SourceName { get; set; }
        public string SourceUrl { get; set; }
        public string Summary { get; set; }

    }
}
