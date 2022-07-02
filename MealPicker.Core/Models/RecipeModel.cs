namespace MealPicker.Core.Models; 

public class RecipeModel {

    public int Id { get; set; }

    public string Title { get; set; }

    public string Image { get; set; }

    public int ReadyInMinutes { get; set; }

    public int Servings { get; set; }

    //authors name
    public string SourceName { get; set; }

    public string SourceUrl { get; set; }

    public IngredientModel[] ExtendedIngredients { get; set; }

    public string Summary { get; set; }

}
