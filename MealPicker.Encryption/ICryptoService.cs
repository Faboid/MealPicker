namespace MealPicker.Encryption {
    public interface ICryptoService : IDisposable {

        string Encrypt(string plainText);
        string Decrypt(string cipherText);

    }
}
