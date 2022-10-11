using MealPicker.Core.Services;
using MealPicker.Utils;

namespace MealPicker.Core.Files;
public interface IKeyHandler {
    void Dispose();
    Task<Option<IConnectionService, KeyHandler.KeyError>> TryGet();
    Task<Option<IConnectionService, KeyHandler.KeyError>> TrySet(string key);
}