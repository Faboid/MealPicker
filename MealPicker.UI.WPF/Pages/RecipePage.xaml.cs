using MealPicker.Core;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Pages; 

/// <summary>
/// Used to get random recipes and showcase them to the user.
/// </summary>
public partial class RecipePage : Page {

    private readonly RecipesNavigator navigator;

    /// <summary>
    /// Is fired to send a message to the user via "sending it up the chain" to the main window.
    /// </summary>
    public event EventHandler<string>? OnSendMessage;

    /// <summary>
    /// Initializes <see cref="RecipePage"/> with the given <paramref name="navigator"/>.
    /// </summary>
    /// <param name="navigator"></param>
    public RecipePage(RecipesNavigator navigator) {
        InitializeComponent();
        this.navigator = navigator;
        Loaded += RecipePage_Loaded;
    }

    private async void RecipePage_Loaded(object sender, RoutedEventArgs e) {
        await SetNextRecipe();
    }

    private async void RandomButton_Click(object sender, RoutedEventArgs e) {
        await SetNextRecipe();
    }

    /// <summary>
    /// Requests a new recipe from <see cref="navigator"/>.
    /// </summary>
    /// <returns></returns>
    private async Task SetNextRecipe() {
        try {
            RandomButton.IsEnabled = false;

            var option = await navigator.NextAsync();
            var result = option.Result();

            if(result == Utils.Options.OptionResult.Some) {
                RecipeView.Recipe = new(option.Or(new()));
            }

            if(result == Utils.Options.OptionResult.Error || result == Utils.Options.OptionResult.None) {
                OnSendMessage?.Invoke(this, option.OrError("The call to get random recipes has failed for an unknown reason."));
            }

        } finally {

            RandomButton.IsEnabled = true;
        }
    }

}
