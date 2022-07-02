using MealPicker.Core.Models;
using MealPicker.Utils;
using System.Net;

namespace MealPicker.Core.Services; 

public interface IConnectionService {

    Task<Option<ListRecipesResult, Requester.FailResult>> GetRandomRecipesAsync(int amount);

}
