using MealPicker.UI.WPF.Commands;
using MealPicker.UI.WPF.Stores;
using System.Windows;
using System.Windows.Input;

namespace MealPicker.UI.WPF.ViewModels;

public class MainViewModel : ViewModelBase {

    private readonly NavigationStore _navigationStore;
    public ViewModelBase? CurrentViewModel => _navigationStore.CurrentViewModel;

    private string _message = "";
    public string Message { get => _message; set => SetAndRaise(nameof(Message), ref _message, value); }

    private bool _messageBoxVisibility = false;
    public bool MessageBoxVisibility { get => _messageBoxVisibility; set => SetAndRaise(nameof(MessageBoxVisibility), ref _messageBoxVisibility, value); }

    public ICommand MinimizeCommand { get; }
    public ICommand ResizeCommand { get; }
    public ICommand CloseCommand { get; }

    public MainViewModel(NavigationStore navigationStore, Window mainWindow) {
        _navigationStore = navigationStore;
        MinimizeCommand = new MinimizeCommand(mainWindow);
        ResizeCommand = new ResizeCommand(mainWindow);
        CloseCommand = new CloseCommand(mainWindow);
        _navigationStore.CurrentViewModelChanged += OnCurrentViewChanged;
    }

    private void OnCurrentViewChanged() {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    protected override void Dispose(bool disposed) {
        _navigationStore.CurrentViewModelChanged -= OnCurrentViewChanged;
        base.Dispose(disposed);
    }
}
