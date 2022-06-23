using MealPicker.Core.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MealPicker.UI.WPF.Views {
    /// <summary>
    /// Interaction logic for RecipeView.xaml
    /// </summary>
    public partial class RecipeView : UserControl {
        public RecipeView() {
            InitializeComponent();
        }

        public static readonly DependencyProperty RecipeProperty =
            DependencyProperty.Register("Recipe", typeof(RecipeModel), typeof(UserControl), new PropertyMetadata(new RecipeModel()));

        public RecipeModel Recipe {
            get => (RecipeModel)GetValue(RecipeProperty);
            set {
                SetValue(RecipeProperty, value);
                LoadRecipe(value);
            }
        }

        private void LoadRecipe(RecipeModel recipe) {

            //set title
            TitleRecipe_TextBlock.Text = recipe.Title;

            //set image
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(recipe.Image);
            image.EndInit();
            RecipeImage.Source = image;

            //add specific info
            ReadyInMinutesTextBlock.Text = recipe.ReadyInMinutes.ToString();
            ServingsTextBlock.Text = recipe.Servings.ToString();

            //refresh ingredients list
            IngredientsListDataGrid.ItemsSource = null;
            IngredientsListDataGrid.ItemsSource = recipe.ExtendedIngredients;

            //add summary
            SummaryTextBox.Text = Core.Conversions.HTML.ToPlainText(recipe.Summary);

            //add authors info
            RecipeAuthorNameTextBlock.Text = recipe.SourceName;
            RecipeLinkButton.Content = recipe.SourceUrl;

        }

        private void RecipeLinkButton_Click(object sender, RoutedEventArgs e) {

            if(Recipe?.SourceUrl == null || !Uri.IsWellFormedUriString(Recipe.SourceUrl, UriKind.Absolute)) {
                return;
            }
            
            try {
                Process.Start(new ProcessStartInfo() {
                    UseShellExecute = true,
                    FileName = Recipe.SourceUrl
                });
            } 
            catch(Win32Exception) { } //thrown when given an unvalid url.

        }
    }
}
