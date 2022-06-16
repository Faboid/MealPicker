using MealPicker.Core.Models;
using System;
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
            throw new NotImplementedException(); //todo - implement link sender
        }
    }
}
