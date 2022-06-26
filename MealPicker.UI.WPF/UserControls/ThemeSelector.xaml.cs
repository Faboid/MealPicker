using MealPicker.UI.WPF.Resources;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.UserControls {
    /// <summary>
    /// Interaction logic for ThemeSelector.xaml
    /// </summary>
    public partial class ThemeSelector : UserControl {
        public ThemeSelector() {
            InitializeComponent();
        }

        private void ThemesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if(ThemesComboBox.SelectedItem is ColorTheme theme) {
                ColorThemes.Painter.Apply(theme);
            } else {
                //todo - handle unexpected result
            }
            ThemesComboBox.Text = ""; //clear text
        }
    }
}
