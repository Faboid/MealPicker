using MealPicker.Core;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Pages; 

/// <summary>
/// Interaction logic for RecipePage.xaml
/// </summary>
public partial class RecipePage : Page {

    private readonly RecipesNavigator navigator;
    public event EventHandler<string>? OnSendMessage;

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

    private async Task SetNextRecipe() {
        try {
            RandomButton.IsEnabled = false;

            var option = await navigator.NextAsync();
            var result = option.Result();

            if(result == Utils.Options.OptionResult.Some) {
                RecipeView.Recipe = option.Or(new());
            }

            if(result == Utils.Options.OptionResult.Error || result == Utils.Options.OptionResult.None) {
                OnSendMessage?.Invoke(this, option.OrError("The call to get random recipes has failed for an unknown reason."));
            }

        } finally {

            RandomButton.IsEnabled = true;
        }
    }

}
