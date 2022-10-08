using MealPicker.Core.Files;
using MealPicker.Encryption;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MealPicker.UI.WPF.HostBuilders;

public static class AddKeyStoragePipelineHostBuilderExtensions {

    public static IHostBuilder AddKeyStoragePipeline(this IHostBuilder hostBuilder) {
        return hostBuilder.ConfigureServices(services => {

            services.AddSingleton<ICryptoServiceFactory, CryptoServiceFactory>();
            services.AddSingleton<IKeyHandlerFactory, KeyHandlerFactory>();

        });
    }

}