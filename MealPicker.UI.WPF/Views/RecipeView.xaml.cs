using MealPicker.UI.WPF.Models;
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
            DependencyProperty.Register("Recipe", typeof(RecipeModel), typeof(UserControl));

        public RecipeModel Recipe {
            get => (RecipeModel)GetValue(RecipeProperty);
            set => SetValue(RecipeProperty, value);
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
