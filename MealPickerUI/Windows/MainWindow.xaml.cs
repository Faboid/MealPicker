using MealPickerLibrary;
using MealPickerLibrary.Queries;
using MealPickerLibrary.Conversions;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MealPickerUI.Windows {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private async void GetRandomRecipebtn_Click(object sender, RoutedEventArgs e) {
            
            try {

                bool isLocked = !await RecipesNavigator.Next();
                if(isLocked) {
                    return;
                }

            } catch (System.Net.Http.HttpRequestException ex) {
                DarkMessageBox.Show("The request has failed!", ex.Message, Dispatcher);
                return;
            }

            LoadRecipe(RecipesNavigator.CurrentRecipe);
        }

        private void LoadRecipe(RecipeModel recipe) {
            //set title
            RecipeTitleTextBlock.Text = recipe.Title;

            //set image
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(recipe.Image);
            image.EndInit();
            RecipeImage.Source = image;

            //add specific info
            ReadyInMinTextBlock.Text = recipe.ReadyInMinutes.ToString();
            ServingsAmountTextBlock.Text = recipe.Servings.ToString();

            //refresh ingredients list
            IngredientsDataGrid.ItemsSource = null;
            IngredientsDataGrid.ItemsSource = recipe.extendedIngredients;

            //add summary
            SummaryTextBlock.Text = HTMLtoPlainText.Convert(recipe.Summary);

            //add authors info
            RecipeAuthorNameTextBlock.Text = recipe.SourceName;
            RecipeLinkBtn.Content = recipe.SourceUrl;
        }

        private void RecipeLinkBtn_Click(object sender, RoutedEventArgs e) {
            try {
                Uri link = new Uri(RecipeLinkBtn.Content as string);
                System.Diagnostics.Process.Start("explorer", link.AbsoluteUri);
            } catch (UriFormatException) {
                DarkMessageBox.Show("Error!", "The button doesn't hold a valid link.", Dispatcher);
            } catch (Exception ex) {
                DarkMessageBox.Show("Error!", ex.Message, Dispatcher);
            }
        }
    }
}
