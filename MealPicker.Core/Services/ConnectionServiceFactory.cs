using MealPicker.Core.Models;
using MealPicker.Utils;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace MealPicker.Core.Services {

    /// <summary>
    /// Handles the creation of a <see cref="IConnectionService"/> with valid <see cref="API_Key"/>.
    /// </summary>
    public class ConnectionServiceFactory {

        /// <summary>
        /// Creates an instance of <see cref="ConnectionServiceFactory"/> with the given <see cref="ILogger"/>.
        /// </summary>
        /// <param name="logger"></param>
        public ConnectionServiceFactory(ILoggerFactory? logger = null) {
            HttpClient client = new() {
                BaseAddress = new Uri("https://api.spoonacular.com/recipes/")
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client = new(client);
            this.logger = logger;
        }

        private readonly ILoggerFactory? logger;
        private readonly Requester client;

        /// <summary>
        /// Tests the key, then returns the result.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>An option with <see cref="IConnectionService"/> on success, or a <see cref="Requester.FailResult"/> on failure.</returns>
        public async Task<Option<IConnectionService, Requester.FailResult>> BuildConnectionService(API_Key key) {
            var query = key.GetQueryTestKey();
            var result = await client.CallAsync<ListRecipesResult>(query).ConfigureAwait(false);
            return result.Bind<IConnectionService>(x => new ConnectionService(client, key, logger));
        }

    }
}
