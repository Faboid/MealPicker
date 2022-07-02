using MealPicker.Core.Models;
using MealPicker.Utils;
using System.Net;

namespace MealPicker.Core.Services;

/// <summary>
/// Handles specific recipe requests.
/// </summary>
public interface IConnectionService {

    /// <summary>
    /// Sends a request for <paramref name="amount"/> number of random recipes.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>An option with <see cref="ListRecipesResult"/> on success, and a <see cref="Requester.FailResult"/> on failure.</returns>
    Task<Option<ListRecipesResult, Requester.FailResult>> GetRandomRecipesAsync(int amount);

}
