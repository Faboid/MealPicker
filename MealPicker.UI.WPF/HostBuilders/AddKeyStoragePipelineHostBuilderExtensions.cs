using MealPicker.Core;
using MealPicker.Core.Files;
using MealPicker.Core.Services;
using MealPicker.Encryption;
using MealPicker.UI.WPF.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MealPicker.UI.WPF.HostBuilders;

public static class AddKeyStoragePipelineHostBuilderExtensions {

    public static IHostBuilder AddKeyStoragePipeline(this IHostBuilder hostBuilder) {
        return hostBuilder.ConfigureServices(services => {

            services.AddSingleton<ICryptoServiceFactory, CryptoServiceFactory>();
            services.AddSingleton<IKeyHandlerFactory, KeyHandlerFactory>();
            services.AddSingleton<IConnectionService>(s => s.GetRequiredService<CryptoContainerStore>().RetrieveConnection());
            services.AddSingleton<RecipesNavigator>();

        });
    }

}