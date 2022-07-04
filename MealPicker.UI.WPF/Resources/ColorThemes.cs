using MealPicker.UI.WPF.Resources.Colors.Themes;
using MealPicker.Utils;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources {

    /// <summary>
    /// A list with all supported color themes.
    /// </summary>
    public enum ColorTheme {
        Dark,
        Light,
        Coffee,
    }

    /// <summary>
    /// Manages the color schemes.
    /// </summary>
    public partial class ColorThemes : ResourceDictionary, IColorThemePainter {

        /// <summary>
        /// Returns a singleton of the <see cref="IColorThemePainter"/> interface to provide methods to change the current theme.
        /// </summary>
        public static IColorThemePainter Painter => BrushesDictionary.Value;

        private static Lazy<ColorThemes> BrushesDictionary { get; } = new Lazy<ColorThemes>(() => new(GetResourceDictionary));

        private static ResourceDictionary GetResourceDictionary => Application
            .Current
            .Resources
            .MergedDictionaries
            .Where(x => x.Source.AbsoluteUri == "pack://application:,,,/Resources/ColorThemes.xaml")
            .First();

        private string BG_100 { get; } = nameof(IColorTheme.BG_100);
        private string BG_200 { get; } = nameof(IColorTheme.BG_200);
        private string BG_300 { get; } = nameof(IColorTheme.BG_300);
        private string BG_400 { get; } = nameof(IColorTheme.BG_400);

        private string FG_100 { get; } = nameof(IColorTheme.FG_100);
        private string FG_200 { get; } = nameof(IColorTheme.FG_200);
        private string FG_300 { get; } = nameof(IColorTheme.FG_300);
        private string FG_400 { get; } = nameof(IColorTheme.FG_400);

        private string Primary_100 { get; } = nameof(IColorTheme.Primary_100);
        private string Highlight_100 { get; } = nameof(IColorTheme.Highlight_100);
        private string Highlight_200 { get; } = nameof(IColorTheme.Highlight_200);
        private string Disabled_100 { get; } = nameof(IColorTheme.Disabled_100);

        public ColorThemes() {
            InitializeComponent();
            dict = this;
            if(!Enum.TryParse<ColorTheme>(settings.Get(themeSettings).Or(""), out var theme)) {
                theme = ColorTheme.Dark; //use as default
            }
            Apply(theme);
        }

        private ColorThemes(ResourceDictionary dictionary) {
            dict = dictionary;
            Apply(0);
        }

        private IColorTheme _theme;
        private const string themeSettings = "theme";
        private readonly Settings settings = new();
        private readonly ResourceDictionary dict;

        [MemberNotNull(nameof(_theme))]
        public void Apply(ColorTheme theme) {
            _theme = theme switch {
                ColorTheme.Dark => new DarkTheme(),
                ColorTheme.Light => new LightTheme(),
                ColorTheme.Coffee => new CoffeeTheme(),
                _ => new DarkTheme(), //default
            };

            OnChangedTheme?.Invoke(this, _theme);

            dict[BG_100] = _theme.BG_100;
            dict[BG_200] = _theme.BG_200;
            dict[BG_300] = _theme.BG_300;
            dict[BG_400] = _theme.BG_400;

            dict[FG_100] = _theme.FG_100;
            dict[FG_200] = _theme.FG_200;
            dict[FG_300] = _theme.FG_300;
            dict[FG_400] = _theme.FG_400;

            dict[Primary_100] = _theme.Primary_100;

            dict[Highlight_100] = _theme.Highlight_100;
            dict[Highlight_200] = _theme.Highlight_200;

            dict[Disabled_100] = _theme.Disabled_100;

            settings.Set(themeSettings, theme.ToString());
        }

        public event EventHandler<IColorTheme>? OnChangedTheme;

    }

    public interface IColorThemePainter {
        public void Apply(ColorTheme theme);
    }

}
