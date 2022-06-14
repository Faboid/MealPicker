using MealPicker.UI.WPF.Resources.Colors.Themes;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources {

    public partial class ColorThemes : ResourceDictionary, IColorThemePainter {

        public static IColorThemePainter Painter => BrushesDictionary.Value;

        private static Lazy<ColorThemes> BrushesDictionary { get; } = new Lazy<ColorThemes>(() => new(GetResourceDictionary));

        private static ResourceDictionary GetResourceDictionary => Application
            .Current
            .Resources
            .MergedDictionaries
            .Where(x => x.Source.AbsoluteUri == "pack://application:,,,/Resources/ColorThemes.xaml")
            .First();

        public string DeepestBG { get; } = nameof(IColorTheme.DeepestBG);
        public string DeepBG { get; } = nameof(IColorTheme.DeepBG);
        public string ForeBG { get; } = nameof(IColorTheme.ForeBG);
        public string LightText { get; } = nameof(IColorTheme.LightText);

        public ColorThemes() {
            InitializeComponent();
            dict = this;
            Apply(0);
        }

        private ColorThemes(ResourceDictionary dictionary) {
            dict = dictionary;
            Apply(0);
        }

        private IColorTheme _theme;
        private readonly ResourceDictionary dict;

        [MemberNotNull(nameof(_theme))]
        public void Apply(ColorTheme theme) {
            _theme = theme switch {
                ColorTheme.Dark => new DarkTheme(),
                ColorTheme.Light => new LightTheme(),
                _ => new DarkTheme(), //default
            };

            OnChangedTheme?.Invoke(this, _theme);

            dict[LightText] = new SolidColorBrush(_theme.LightText);
            dict[DeepestBG] = new SolidColorBrush(_theme.DeepestBG);
        }

        public event EventHandler<IColorTheme>? OnChangedTheme;

    }

    public interface IColorThemePainter {
        public void Apply(ColorTheme theme);
    }

    public enum ColorTheme {
        Dark,
        Light
    }

}
