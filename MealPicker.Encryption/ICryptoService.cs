namespace MealPicker.Encryption {

    /// <summary>
    /// Provides an interface used to build encryption and decryption methods.
    /// </summary>
    public interface ICryptoService : IDisposable {

        /// <summary>
        /// Encrypts the given string.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        string Encrypt(string plainText);

        /// <summary>
        /// Decrypts the given string.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        string Decrypt(string cipherText);

    }
}
