using MealPicker.Core.Models;
using MealPicker.Utils;
using System.Net;

namespace MealPicker.Core.Services; 

public interface IConnectionService {

    public record struct FailResult(HttpStatusCode StatusCode, string? ReasonPhrase);
    Task<Option<ListRecipesResult, FailResult>> GetRandomRecipesAsync(int amount);

}
