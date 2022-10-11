using MealPicker.UI.WPF.HostBuilders;
using MealPicker.UI.WPF.Stores;
using MealPicker.UI.WPF.ViewModels;
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
                .AddUtilities()
                .AddUIComponents()
                .AddKeyStoragePipeline()
                .AddStores()
                .AddMainWindow()
                .AddViewModels()
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e) {

            _host.Start();
            ViewModelBase startingVM;
            startingVM = _host.Services.GetRequiredService<LoginViewModel>();
            _host.Services.GetRequiredService<NavigationStore>().CurrentViewModel = startingVM;
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            
            base.OnStartup(e);
        }
    }
}
