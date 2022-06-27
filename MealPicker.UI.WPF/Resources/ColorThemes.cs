using MealPicker.UI.WPF.Resources.Colors.Themes;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources {

    public enum ColorTheme {
        Dark,
        Light,
        Coffee,
    }

    public partial class ColorThemes : ResourceDictionary, IColorThemePainter {

        public static IColorThemePainter Painter => BrushesDictionary.Value;

        private static Lazy<ColorThemes> BrushesDictionary { get; } = new Lazy<ColorThemes>(() => new(GetResourceDictionary));

        private static ResourceDictionary GetResourceDictionary => Application
            .Current
            .Resources
            .MergedDictionaries
            .Where(x => x.Source.AbsoluteUri == "pack://application:,,,/Resources/ColorThemes.xaml")
            .First();

        public string BG_100 { get; } = nameof(IColorTheme.BG_100);
        public string BG_200 { get; } = nameof(IColorTheme.BG_200);
        public string BG_300 { get; } = nameof(IColorTheme.BG_300);
        public string BG_400 { get; } = nameof(IColorTheme.BG_400);

        public string FG_100 { get; } = nameof(IColorTheme.FG_100);
        public string FG_200 { get; } = nameof(IColorTheme.FG_200);
        public string FG_300 { get; } = nameof(IColorTheme.FG_300);
        public string FG_400 { get; } = nameof(IColorTheme.FG_400);

        public string Primary_100 { get; } = nameof(IColorTheme.Primary_100);
        public string Highlight_100 { get; } = nameof(IColorTheme.Highlight_100);
        public string Highlight_200 { get; } = nameof(IColorTheme.Highlight_200);
        public string Disabled_100 { get; } = nameof(IColorTheme.Disabled_100);



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

        }

        public event EventHandler<IColorTheme>? OnChangedTheme;

    }

    public interface IColorThemePainter {
        public void Apply(ColorTheme theme);
    }

}
