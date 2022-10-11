using MealPicker.UI.WPF.Resources;
using Serilog;
using System.Windows.Controls;

namespace MealPicker.UI.WPF.Components {
    /// <summary>
    /// Represents a <see cref="ComboBox"/> that allows selecting the current <see cref="ColorTheme"/>.
    /// </summary>
    public partial class ThemeSelector : UserControl {

        public ILogger Logger = Log.Logger;

        public ThemeSelector() {
            InitializeComponent();
        }

        private void ThemesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            if(ThemesComboBox.SelectedItem == null) {
                Logger?.Warning("The themes combo box's selected item is null.");
                return;
            }

            if(ThemesComboBox.SelectedItem is ColorTheme theme) {
                ColorThemes.Painter.Apply(theme);
            } else {
                Logger?.Error($"The themes combo box's selected item is not a ColorTheme, being instead {ThemesComboBox.SelectedItem.GetType()}.");
            }
        }
    }
}
