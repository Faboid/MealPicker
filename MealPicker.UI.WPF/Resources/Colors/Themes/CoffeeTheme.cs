using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources.Colors.Themes {
    internal class CoffeeTheme : IColorTheme {

        public SolidColorBrush BG_100 { get; } = "#0E0504".ToSolidBrush();
        public SolidColorBrush BG_200 { get; } = "#4D2B1E".ToSolidBrush();
        public SolidColorBrush BG_300 { get; } = "#794028".ToSolidBrush();
        public SolidColorBrush BG_400 { get; } = "#a85938".ToSolidBrush();

        private const string white = "FFFFFF";
        public SolidColorBrush FG_100 { get; } = white.ToSolidBrush();
        public SolidColorBrush FG_200 { get; } = white.ToSolidBrush().WithOpacity(0.90);
        public SolidColorBrush FG_300 { get; } = white.ToSolidBrush().WithOpacity(0.90);
        public SolidColorBrush FG_400 { get; } = white.ToSolidBrush().WithOpacity(0.90);

        public SolidColorBrush Primary_100 { get; } = "4B1F0E".ToSolidBrush();

        public SolidColorBrush Highlight_100 { get; } = "606060".ToSolidBrush();
        public SolidColorBrush Highlight_200 { get; } = "696969".ToSolidBrush();

        public SolidColorBrush Disabled_100 { get; } = "BFBFBF".ToSolidBrush();

    }
}
