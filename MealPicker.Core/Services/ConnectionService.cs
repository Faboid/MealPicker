using MealPicker.Core.Models;
using MealPicker.Utils;

namespace MealPicker.Core.Services;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class ConnectionService : IConnectionService {

    internal ConnectionService(ILogger logger, Requester client, API_Key key) {
        this.key = key;
        this.client = client;
        this.logger = logger;
    }

    private readonly ILogger logger;
    private readonly API_Key key;
    private readonly Requester client;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="amount"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Option<ListRecipesResult, Requester.FailResult>> GetRandomRecipesAsync(int amount) {
        logger.LogInfo($"Requesting {amount} random recipes.");
        return await client.CallAsync<ListRecipesResult>(key.GetQueryRandomRecipes(amount)).ConfigureAwait(false);
    }
}