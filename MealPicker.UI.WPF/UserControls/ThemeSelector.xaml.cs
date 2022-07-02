using MealPicker.UI.WPF.Resources;
using MealPicker.Utils;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.UserControls {
    /// <summary>
    /// Interaction logic for ThemeSelector.xaml
    /// </summary>
    public partial class ThemeSelector : UserControl {

        public ILogger? Logger { get; set; }

        public ThemeSelector() {
            InitializeComponent();
        }

        private void ThemesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if(ThemesComboBox.SelectedItem == null) {
                Logger?.LogWarning("The themes combo box's selected item is null.");
                return;
            }

            if(ThemesComboBox.SelectedItem is ColorTheme theme) {
                ColorThemes.Painter.Apply(theme);
            } else {
                Logger?.LogError($"The themes combo box's selected item is not a ColorTheme, being instead {ThemesComboBox.SelectedItem.GetType()}.");
            }
        }
    }
}
