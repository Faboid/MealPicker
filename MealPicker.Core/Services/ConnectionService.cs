using System.Net.Http.Headers;
using System.Text.Json;
using MealPicker.Core.Models;
using MealPicker.Utils;

namespace MealPicker.Core.Services;

//static members
public partial class ConnectionService {

    private static readonly HttpClient client;

    static ConnectionService() {
        client = new() {
            BaseAddress = new Uri("https://api.spoonacular.com/recipes/")
        };
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    /// <summary>
    /// Calls an API by using the given path. Throws an exception if the request fails.
    /// </summary>
    /// <typeparam name="T">The object that will be returned after converting the calls' response values.</typeparam>
    /// <param name="path">The path to call the API.</param>
    /// <returns>An instantiated object filled with the properties gained from the API.</returns>
    /// <exception cref="HttpRequestException"></exception>
    private static async Task<Option<T, IConnectionService.FailResult>> CallAPIAsync<T>(string path) where T : class {
        using HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false);
        if(response.IsSuccessStatusCode == false) {
            return new IConnectionService.FailResult(response.StatusCode, response.ReasonPhrase);
        }

        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true};
        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return (await JsonSerializer.DeserializeAsync<T>(stream, options).ConfigureAwait(false)) ?? Option.None<T, IConnectionService.FailResult>();
    }

    //todo - extract this to a factory class
    public async static Task<Option<ConnectionService, IConnectionService.FailResult>> CreateConnectionAsync(API_Key key) {
        var query = key.GetQueryTestKey();
        var result = await CallAPIAsync<ListRecipesResult>(query).ConfigureAwait(false);
        return result.Bind<ConnectionService>(x => new ConnectionService(key));
    }

}

//non-static members
public partial class ConnectionService : IConnectionService {

    private ConnectionService(API_Key key) {
        this.key = key;
    }

    private readonly API_Key key;

    public async Task<Option<ListRecipesResult, IConnectionService.FailResult>> GetRandomRecipesAsync(int amount) {
        return await CallAPIAsync<ListRecipesResult>(key.GetQueryRandomRecipes(amount)).ConfigureAwait(false);
    }
}