using MealPicker.Encryption.Cryptography;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

[assembly: InternalsVisibleTo("MealPicker.Encryption.Tests")]
namespace MealPicker.Encryption {

    /// <summary>
    /// Handles encryption and decryption of strings by using versioning, custom-length salt, and a key.
    /// </summary>
    public class CryptoService : ICryptoService, IDisposable {
        //The encryption methods are built upon a derived version of "A Gazhal"'s answer https://stackoverflow.com/a/27484425/16018958

        private const string currentVersion = "1.00";
        private readonly Versioning versioning;
        private readonly SaltGenerator saltGenerator;
        private readonly Key key;

        /// <summary>
        /// Creates an instance of <see cref="CryptoService"/> with the given <paramref name="password"/>. Then, clears the given password with /0.
        /// </summary>
        /// <param name="password"></param>
        public CryptoService(char[] password) : this(password, currentVersion) { }

        /// <summary>
        /// Creates an instance of <see cref="CryptoService"/> with the given <paramref name="password"/> and sets the current version to <paramref name="version"/>. 
        /// Lastly, clears the given password by setting every value to \0.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="version"></param>
        internal CryptoService(char[] password, string version) : this(new Key(password), version) { }

        /// <summary>
        /// Creates an instance of <see cref="CryptoService"/> with the given <paramref name="key"/>.
        /// </summary>
        /// <param name="key"></param>
        public CryptoService(Key key) : this(key, currentVersion) { }

        /// <summary>
        /// Creates an instance of <see cref="CryptoService"/> with the given <paramref name="key"/> and sets the current version to <paramref name="version"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="version"></param>
        internal CryptoService(Key key, string version) {
            this.key = key;
            saltGenerator = new SaltGenerator(key.Get());
            versioning = new Versioning(version);
        }

        /// <summary>
        /// Encrypts <paramref name="plainText"/> using the given <see cref="key"/> and applies a custom salt and version to it. Subsequent decryption must use the same password.
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Encrypt(string plainText) {

            byte[] bytes = Encoding.Unicode.GetBytes(plainText);
            byte[] salt = saltGenerator.CreateSalt();

            using Aes encryptor = GetAes(salt, versioning.GetVersion());
            using var ms = new MemoryStream();
            WriteToStream(ms, encryptor.CreateEncryptor(), bytes);

            byte[] output = salt.Concat(ms.ToArray()).ToArray();
            output = versioning.AddVersion(output);
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// Decrypts <paramref name="cipherText"/> using the given <see cref="key"/>. Reads its version to ensure backward compatibility. <br/><br/>
        /// Attempting decryption of text that wasn't encrypted with <see cref="Encrypt(string)"/> will result in <see cref="ArgumentException"/>. <br/>
        /// Attempting decryption of text that was encrypted with a different password will result in <see cref="CryptographicException"/>.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns>A decrypted string.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="CryptographicException"></exception>
        public string Decrypt(string cipherText) {

            string version = Versioning.ExtractVersion(ref cipherText);
            byte[] salt = saltGenerator.ExtractSalt(ref cipherText);
            cipherText = cipherText.Replace(" ", "+");
            byte[] bytes = Convert.FromBase64String(cipherText);

            using Aes encryptor = GetAes(salt, version);
            using var ms = new MemoryStream();
            WriteToStream(ms, encryptor.CreateDecryptor(), bytes);
            return Encoding.Unicode.GetString(ms.ToArray());
        }

        private Aes GetAes(byte[] salt, string version) {
            Aes aes = Aes.Create();
            using var rfc = new Rfc2898DeriveBytes(Encoding.Unicode.GetBytes(key.Get()), salt, Versioning.GetRfcDerivedBytesIterations(version));
            aes.Key = rfc.GetBytes(32);
            aes.IV = rfc.GetBytes(16);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            return aes;
        }

        private static void WriteToStream(MemoryStream ms, ICryptoTransform transform, byte[] bytes) {
            using CryptoStream cs = new(ms, transform, CryptoStreamMode.Write);
            cs.Write(bytes);
        }

        public void Dispose() {
            key.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}