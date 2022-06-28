using MealPicker.Core;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Pages; 

/// <summary>
/// Interaction logic for RecipePage.xaml
/// </summary>
public partial class RecipePage : Page {

    private readonly RecipesNavigator navigator;

    public RecipePage(RecipesNavigator navigator) {
        InitializeComponent();
        this.navigator = navigator;
    }

    private async void RandomButton_Click(object sender, RoutedEventArgs e) {
        await SetNextRecipe();
    }

    private async Task SetNextRecipe() {
        try {
            RandomButton.IsEnabled = false;

            var option = await navigator.NextAsync();
            RecipeView.Recipe = option.Match(
                some => some,
                () => RecipeView.Recipe
            );

        } finally {

            RandomButton.IsEnabled = true;
        }
    }

}
