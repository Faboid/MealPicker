using MealPicker.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MealPicker.UI.WPF.HostBuilders;

public static class AddUtilitiesHostBuilderExtensions {

    public static IHostBuilder AddUtilities(this IHostBuilder hostBuilder) {
        return hostBuilder.ConfigureServices(services => {

            services.AddSingleton<ILinkOpener, LinkOpener>();

        });
    }

}