using MealPicker.UI.WPF.Stores;
using MealPicker.UI.WPF.ViewModels;
using Microsoft.Extensions.Logging;
using System;

namespace MealPicker.UI.WPF.Services;

/// <summary>
/// Provides methods to navigate to <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The viewmodel to navigate to.</typeparam>
public class NavigationService<T> where T : ViewModelBase {

    private readonly ILogger<NavigationService<T>>? _logger;
    private readonly NavigationStore _navigationStore;
    private readonly Func<T> _navigationFunction;

    public NavigationService(NavigationStore navigationStore, Func<T> navigationFunction, ILogger<NavigationService<T>>? logger = null) {
        _navigationStore = navigationStore;
        _navigationFunction = navigationFunction;
        _logger = logger;
    }

    /// <summary>
    /// Navigates to <typeparamref name="T"/>.
    /// </summary>
    /// <param name="disposeCurrent">Whether the current viewmodel should be disposed.</param>
    public void Navigate(bool disposeCurrent) {
        if(disposeCurrent) {
            _navigationStore.CurrentViewModel?.Dispose();
        }
        var newVM = _navigationFunction.Invoke();
        _logger?.LogDebug("Navigating from {Current}, to {New}", _navigationStore.CurrentViewModel?.GetType().Name, newVM.GetType().Name);
        _navigationStore.CurrentViewModel = newVM;
    }

}