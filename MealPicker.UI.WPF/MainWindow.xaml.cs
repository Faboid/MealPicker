using MealPicker.Core;
using MealPicker.Core.Services;
using MealPicker.UI.WPF.Pages;
using MealPicker.UI.WPF.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MealPicker.UI.WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            KeyHandlerPage page = new();
            page.CloseAndReturn += OnConnectionObtained;
            PageContainer.Navigate(page);
        }

        private void OnConnectionObtained(object? sender, ConnectionService e) {
            RecipesNavigator nav = new(e);
            Dispatcher.Invoke(() => {
                RecipePage page = new(nav);
                PageContainer.Navigate(page);
            });
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e) {
            WindowState = WindowState switch {
                WindowState.Normal => WindowState.Maximized,
                WindowState.Maximized => WindowState.Normal,
                _ => WindowState.Normal
            };
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
