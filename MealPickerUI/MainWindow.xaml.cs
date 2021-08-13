using MealPickerLibrary;
using MealPickerLibrary.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MealPickerUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        RecipeModel current { get => RecipesNavigator.CurrentRecipe; }

        private async void GetRandomRecipebtn_Click(object sender, RoutedEventArgs e) {
            bool result = await RecipesNavigator.Next();

            if(result is false) {
                //tell user something failed
            }

            //set title
            RecipeTitleTextBlock.Text = current.Title;

            //set image
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(current.Image);
            image.EndInit();
            RecipeImage.Source = image;

            //add specific info
            ReadyInMinTextBlock.Text = current.ReadyInMinutes.ToString();
            ServingsAmountTextBlock.Text = current.Servings.ToString();

            //refresh ingredients list
            IngredientsDataGrid.ItemsSource = null;
            IngredientsDataGrid.ItemsSource = current.extendedIngredients;

            //add summary
            SummaryTextBlock.Text = current.Summary;

            //add authors info
            RecipeAuthorNameTextBlock.Text = current.SourceName;
            RecipeLinkBtn.Content = current.SourceUrl;
        }



    }
}
