using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources {
    public partial class ColorThemes : ResourceDictionary, IColorThemePainter {

        public static IColorThemePainter Painter => brushesDictionary.Value;

        private static Lazy<ColorThemes> brushesDictionary { get; } = new Lazy<ColorThemes>(() => new(GetResourceDictionary));

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
        private ResourceDictionary dict;

        [MemberNotNull(nameof(_theme))]
        public void Apply(ColorTheme theme) {
            _theme = theme switch {
                ColorTheme.Dark => new DarkTheme(),
                ColorTheme.Light => new LightTheme(),
                _ => new DarkTheme(), //default
            };

            OnChangedTheme?.Invoke(this, _theme);

            dict["LightText"] = new SolidColorBrush(_theme.LightText);
            dict["DeepestBG"] = new SolidColorBrush(_theme.DeepestBG);
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

    public class DarkTheme : IColorTheme {
        public Color DeepestBG => "#FF272121".ToColor();
        public Color DeepBG => throw new NotImplementedException();
        public Color ForeBG => throw new NotImplementedException();
        public Color LightText => "FFFFFFFF".ToColor();

    }

    public class LightTheme : IColorTheme {
        public Color DeepestBG => "FFFFFFFF".ToColor();
        public Color DeepBG => throw new NotImplementedException();
        public Color ForeBG => throw new NotImplementedException();
        public Color LightText => "FF272121".ToColor();

    }

    public interface IColorTheme {

        //temp names
        Color DeepestBG { get; }
        Color DeepBG { get; }
        Color ForeBG { get; }
        Color LightText { get; }

    }

    internal static class Extensions {

        //todo - test this method
        public static Color ToColor(this string hex) {

            if(hex[0] == '#') {
                hex = hex[1..];
            }

            if(hex.Length == 6) {
                var color = Color
                    .FromRgb(
                        Convert.ToByte(hex.Substring(0, 2), 16),
                        Convert.ToByte(hex.Substring(2, 2), 16),
                        Convert.ToByte(hex.Substring(4, 2), 16)
                    );
                return color;
            }

            if(hex.Length == 8) {
                var color = Color
                    .FromArgb(
                        Convert.ToByte(hex.Substring(0, 2), 16),
                        Convert.ToByte(hex.Substring(2, 2), 16),
                        Convert.ToByte(hex.Substring(4, 2), 16),
                        Convert.ToByte(hex.Substring(6, 2), 16)
                    );
                return color;
            }

            throw new ArgumentException($"The given hex ({hex}) is not valid.", nameof(hex));
        }

    }

}
