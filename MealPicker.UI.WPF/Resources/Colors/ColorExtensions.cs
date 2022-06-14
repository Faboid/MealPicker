using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MealPicker.UI.WPF.Resources.Colors {
    internal static class ColorExtensions {

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
