using MealPicker.UI.WPF.Stores;
using MealPicker.UI.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MealPicker.UI.WPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        public App() {

        }

        protected override void OnStartup(StartupEventArgs e) {

            var navigationStore = new NavigationStore();
            var window = new MainWindow();
            window.DataContext = new MainViewModel(navigationStore, window);
            MainWindow = window;
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
