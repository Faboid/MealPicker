using MealPicker.UI.WPF.Models;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Views {

    /// <summary>
    /// Represents a view for <see cref="RecipeModel"/>.
    /// </summary>
    public partial class RecipeView : UserControl {

        /// <summary>
        /// Initializes <see cref="RecipeView"/>.
        /// </summary>
        public RecipeView() {
            InitializeComponent();
        }

        private static readonly DependencyProperty RecipeProperty =
            DependencyProperty.Register("Recipe", typeof(RecipeModel), typeof(UserControl));

        /// <summary>
        /// The current recipe to show.
        /// </summary>
        public RecipeModel Recipe {
            get => (RecipeModel)GetValue(RecipeProperty);
            set => SetValue(RecipeProperty, value);
        }

        /// <summary>
        /// Follows the current <see cref="Recipe.SourceUrl"/> by opening a browser tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
