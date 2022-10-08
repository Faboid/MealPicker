using MealPicker.Core.Services;
using MealPicker.UI.WPF.Services;
using MealPicker.UI.WPF.Stores;
using MealPicker.UI.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace MealPicker.UI.WPF.HostBuilders;

public static class AddViewModelsHostBuilderExtensions {

    public static IHostBuilder AddMainWindow(this IHostBuilder hostBuilder) {
        return hostBuilder.ConfigureServices(services => {

            services.AddTransient<Func<Window, MainViewModel>>((s) => (win) => new MainViewModel(s.GetRequiredService<NavigationStore>(), win, s.GetRequiredService<INotificationService>()));

            services.AddSingleton((s) => {
                var window = new MainWindow();
                window.DataContext = s.GetRequiredService<Func<Window, MainViewModel>>().Invoke(window);
                return window;
            });

        });
    }

    public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder) {
        return hostBuilder.ConfigureServices(services => {

            services.AddSingleton<Func<SignupViewModel>>(s => s.GetRequiredService<SignupViewModel>);
            services.AddSingleton<Func<IConnectionService, RecipeGeneratorViewModel>>(s => new()); //todo - implement

            services.AddSingleton<NavigationService<SignupViewModel>>();
            services.AddSingleton<NavigationService<RecipeGeneratorViewModel, IConnectionService>>();

            services.AddTransient<LoginViewModel>();
            services.AddTransient<SignupViewModel>();

        });
    }

}