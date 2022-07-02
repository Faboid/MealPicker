using MealPicker.Utils;
using System.Net;
using System.Text.Json;

namespace MealPicker.Core.Services {

    /// <summary>
    /// Handles GET calls for JSON objects.
    /// </summary>
    public class Requester { 

        public record struct FailResult(HttpStatusCode StatusCode, string? ReasonPhrase);

        //todo - dispose HttpClient
        private readonly HttpClient client;

        /// <summary>
        /// Creates an instance of <see cref="Requester"/> with the given <see cref="HttpClient"/>. 
        /// Every call will use the given instance.
        /// </summary>
        /// <param name="client"></param>
        public Requester(HttpClient client) {
            this.client = client;
        }


        /// <summary>
        /// Calls the client by using the given path. Returns an <see cref="Option{TValue, TError}"/> error option on failure.
        /// </summary>
        /// <typeparam name="T">The object that will be returned on success.</typeparam>
        /// <param name="path">The request's arguments.</param>
        /// <returns>An option that reports the success of the call.</returns>
        public async Task<Option<T, FailResult>> CallAsync<T>(string path) where T : class {
            using HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false);
            if(response.IsSuccessStatusCode == false) {
                return new FailResult(response.StatusCode, response.ReasonPhrase);
            }

            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            return (await JsonSerializer.DeserializeAsync<T>(stream, options).ConfigureAwait(false)) ?? Option.None<T, FailResult>();
        }

    }
}
