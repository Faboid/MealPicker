namespace MealPicker.Core.Files;

public interface IKeyHandlerFactory {
    IKeyHandler CreateKeyHandler(char[] password);
}