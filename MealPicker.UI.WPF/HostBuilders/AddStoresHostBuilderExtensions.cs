using MealPicker.UI.WPF.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MealPicker.UI.WPF.HostBuilders;

public static class AddStoresHostBuilderExtensions {

    public static IHostBuilder AddStores(this IHostBuilder hostBuilder) {
        return hostBuilder.ConfigureServices(services => {

            services.AddSingleton<NavigationStore>();
            services.AddSingleton<CryptoContainerStore>();
            services.AddSingleton<RecipeStore>();

        });
    }

}
