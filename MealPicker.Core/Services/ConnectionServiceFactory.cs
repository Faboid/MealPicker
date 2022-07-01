using MealPicker.Core.Models;
using MealPicker.Utils;
using System.Net.Http.Headers;

namespace MealPicker.Core.Services {
    public class ConnectionServiceFactory {

        public ConnectionServiceFactory(ILogger logger) {
            HttpClient client = new() {
                BaseAddress = new Uri("https://api.spoonacular.com/recipes/")
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client = new(client);
            this.logger = logger;
        }

        private readonly ILogger logger;
        private readonly Requester client;

        public async Task<Option<IConnectionService, Requester.FailResult>> BuildConnectionService(API_Key key) {
            var query = key.GetQueryTestKey();
            var result = await client.CallAsync<ListRecipesResult>(query).ConfigureAwait(false);
            return result.Bind<IConnectionService>(x => new ConnectionService(logger, client, key));
        }

    }
}
