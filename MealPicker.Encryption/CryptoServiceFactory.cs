namespace MealPicker.Encryption;

public class CryptoServiceFactory : ICryptoServiceFactory {

    public ICryptoService CreateCryptoService(char[] password) {
        return new CryptoService(password);
    }

}