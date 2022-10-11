using MealPicker.UI.WPF.Commands;
using MealPicker.UI.WPF.Services;
using MealPicker.UI.WPF.Stores;
using Microsoft.Extensions.Logging;
using System.Windows;
using System.Windows.Input;

namespace MealPicker.UI.WPF.ViewModels;

public class RecipeGeneratorViewModel : ViewModelBase {

	public RecipeViewModel RecipeViewModel { get; } = new();
	public ICommand GenerateRandomRecipeCommand { get; }
	public ICommand LoadedCommand { get; }

	public RecipeGeneratorViewModel(RecipeStore recipeStore, INotificationService notificationService, ILoggerFactory? loggerFactory = null) {
		GenerateRandomRecipeCommand = new GetNextRandomRecipeCommand(this, recipeStore, notificationService, loggerFactory);
		LoadedCommand = new RelayCommand(() => GenerateRandomRecipeCommand.Execute(null));
	}

}