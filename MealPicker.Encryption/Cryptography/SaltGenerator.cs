using MealPicker.Encryption.Random;
using System.Security.Cryptography;

namespace MealPicker.Encryption.Cryptography {

    /// <summary>
    /// Handles generating salt.
    /// </summary>
    internal class SaltGenerator {

        readonly private int length;
        readonly private int minLength = 32;
        readonly private int maxLength = 64;

        /// <summary>
        /// Creates and instance of <see cref="SaltGenerator"/> based on the given <paramref name="password"/>.
        /// </summary>
        /// <param name="password"></param>
        public SaltGenerator(char[] password) {
            int passValue = password.Sum(x => x / (password.Length / 2));
            length = new SaltRandom(minLength, maxLength, passValue).Next();
        }

        /// <summary>
        /// Generates a random salt. The length is dependent on the password given on initialization.
        /// </summary>
        /// <returns></returns>
        public byte[] CreateSalt() => RandomNumberGenerator.GetBytes(length);

        /// <summary>
        /// Extracts the salt from an encrypted string. 
        /// Due to the variable-length nature of the pre-generated salt, 
        /// it's necessary to use the same password used previously for encryption.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] ExtractSalt(ref string text) {
            byte[] bytes = Convert.FromBase64String(text);
            byte[] salt = bytes.Take(length).ToArray();
            text = Convert.ToBase64String(bytes.Skip(length).ToArray());
            return salt;
        }

    }
}
