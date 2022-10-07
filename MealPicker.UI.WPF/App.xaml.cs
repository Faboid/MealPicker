using MealPicker.UI.WPF.HostBuilders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MealPicker.UI.WPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private readonly IHost _host;

        public App() {
            _host = Host
                .CreateDefaultBuilder()
                .AddStores()
                .AddMainWindow()
                .AddViewModels()
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e) {

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
