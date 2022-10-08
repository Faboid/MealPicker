using MealPicker.Core.Models;
using MealPicker.Utils;
using Microsoft.Extensions.Logging;

namespace MealPicker.Core.Services;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class ConnectionService : IConnectionService {

    internal ConnectionService(Requester client, API_Key key, ILoggerFactory? loggerFactory = null) {
        _key = key;
        _client = client;
        _logger = loggerFactory?.CreateLogger<ConnectionService>();
    }

    private readonly ILogger<ConnectionService>? _logger;
    private readonly API_Key _key;
    private readonly Requester _client;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="amount"><inheritdoc/></param>
    /// <returns><inheritdoc/></returns>
    public async Task<Option<ListRecipesResult, Requester.FailResult>> GetRandomRecipesAsync(int amount) {
        _logger?.LogInformation($"Requesting {amount} random recipes.");
        return await _client.CallAsync<ListRecipesResult>(_key.GetQueryRandomRecipes(amount)).ConfigureAwait(false);
    }
}