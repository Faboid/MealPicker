using MealPicker.UI.WPF.Services;
using MealPicker.UI.WPF.Stores;
using MealPicker.UI.WPF.ViewModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MealPicker.UI.WPF.Commands;

public class GetNextRandomRecipeCommand : AsyncCommandBase {

    private readonly RecipeStore _recipeStore;
    private readonly RecipeGeneratorViewModel _recipeGeneratorViewModel;
    private readonly INotificationService _notificationService;
    private readonly ILogger<GetNextRandomRecipeCommand>? _logger;

    public GetNextRandomRecipeCommand(RecipeGeneratorViewModel recipeGeneratorViewModel, 
                                        RecipeStore recipeStore,
                                        INotificationService notificationService,
                                        ILoggerFactory? loggerfactory = null) {
        _recipeStore = recipeStore;
        _recipeGeneratorViewModel = recipeGeneratorViewModel;
        _notificationService = notificationService;
        _logger = loggerfactory?.CreateLogger<GetNextRandomRecipeCommand>();
    }

    protected override async Task ExecuteAsync(object? parameter) {

        _logger?.LogInformation("Generating new random recipe...");
        var result = await _recipeStore.GetNextRandomRecipeAsync();

        var recipe = result.Or(default!);
        if(recipe != null) {
            _recipeGeneratorViewModel.RecipeViewModel.Recipe = recipe;
            return;
        }

        var message = result.Match(some => "There has been a problem when returning the recipe.", error => error, () => "There has been an unknown problem.");
        _notificationService.Send(message);
        _logger?.LogWarning("{ErrorMessage}", message);

    }
}