namespace MealPicker.Encryption;

public interface ICryptoServiceFactory {
    ICryptoService CreateCryptoService(char[] password);
}