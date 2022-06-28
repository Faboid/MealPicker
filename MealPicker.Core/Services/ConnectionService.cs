using MealPicker.Core.Models;
using MealPicker.Utils;

namespace MealPicker.Core.Services;

public class ConnectionService : IConnectionService {

    internal ConnectionService(Requester client, API_Key key) {
        this.key = key;
        this.client = client;
    }

    private readonly API_Key key;
    private readonly Requester client;

    public async Task<Option<ListRecipesResult, Requester.FailResult>> GetRandomRecipesAsync(int amount) {
        return await client.CallAsync<ListRecipesResult>(key.GetQueryRandomRecipes(amount)).ConfigureAwait(false);
    }
}