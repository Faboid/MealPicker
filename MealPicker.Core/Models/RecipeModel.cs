namespace MealPicker.Core.Models; 

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
    public IngredientModel[] ExtendedIngredients { get; set; }
    public string Summary { get; set; }

}
