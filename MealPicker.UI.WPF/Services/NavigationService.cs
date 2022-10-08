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

    public NavigationService(NavigationStore navigationStore, Func<T> navigationFunction, ILoggerFactory? loggerFactory = null) {
        _navigationStore = navigationStore;
        _navigationFunction = navigationFunction;
        _logger = loggerFactory?.CreateLogger<NavigationService<T>>();
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

/// <summary>
/// Provides methods to navigate to <typeparamref name="TViewModel"/>, by giving it a <typeparamref name="TArgument"/>.
/// </summary>
/// <typeparam name="TViewModel">The viewmodel to navigate to.</typeparam>
/// <typeparam name="TArgument">The argument type to give to the viewmodel.</typeparam>
public class NavigationService<TViewModel, TArgument> where TViewModel : ViewModelBase {

    private readonly ILogger<NavigationService<TViewModel, TArgument>>? _logger;
    private readonly NavigationStore _navigationStore;
    private readonly Func<TArgument, TViewModel> _navigationFunction;

    public NavigationService(NavigationStore navigationStore, Func<TArgument, TViewModel> navigationFunction, ILoggerFactory? loggerFactory = null) {
        _navigationStore = navigationStore;
        _navigationFunction = navigationFunction;
        _logger = loggerFactory?.CreateLogger<NavigationService<TViewModel, TArgument>>();
    }

    /// <summary>
    /// Navigates to <typeparamref name="TViewModel"/> by giving it <typeparamref name="TArgument"/>.
    /// </summary>
    /// <param name="argument">The argument given to the <see cref="Func{T, TResult}"/> returning the viewmodel.</param>
    /// <param name="disposeCurrent">Whether the current viewmodel should be disposed.</param>
    public void Navigate(TArgument argument, bool disposeCurrent) {
        if(disposeCurrent) {
            _navigationStore.CurrentViewModel?.Dispose();
        }
        var newVM = _navigationFunction.Invoke(argument);
        _logger?.LogDebug("Navigating from {Current}, to {New}", _navigationStore.CurrentViewModel?.GetType().Name, newVM.GetType().Name);
        _navigationStore.CurrentViewModel = newVM;
    }

}