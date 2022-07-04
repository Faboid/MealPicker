using System.Text;

namespace MealPicker.Encryption.Cryptography {

    /// <summary>
    /// Provides a way to version the encrypted strings. It is used to ensure backward compatibility.
    /// </summary>
    internal class Versioning {

        private string CurrentVersion { get; init; }
        private const char openingChar = '(';
        private const char closingChar = ')';

        /// <summary>
        /// Initializes an instance of <see cref="Versioning"/> with the given <paramref name="version"/>.
        /// </summary>
        /// <param name="version"></param>
        public Versioning(string version) {
            //checks if the given version exists
            _ = GetRfcDerivedBytesIterations(version);
            CurrentVersion = version;
        }

        /// <summary>
        /// Gets the current version.
        /// </summary>
        /// <returns></returns>
        public string GetVersion() {
            return CurrentVersion;
        }

        /// <summary>
        /// Adds the version bytes to the start of a byte array.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] AddVersion(byte[] text) {
            byte[] versionAsBytes = Encoding.Unicode.GetBytes($"{openingChar}{GetVersion()}{closingChar}");
            return versionAsBytes.Concat(text).ToArray();
        }

        /// <summary>
        /// Gets the number of iterations used for the given <paramref name="version"/>. Throws <see cref="NotImplementedException"/> when given a non-existant version.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static int GetRfcDerivedBytesIterations(string version) => version switch {
            "test.1" => 1000,
            "test.2" => 20000,
            "1.00" => 500000,
            _ => throw new NotImplementedException($"The given version hasn't been implemented yet: {version}")
        };

        /// <summary>
        /// Extracts the text of the version from the encrypted string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>The version text that's been extracted by the string.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ExtractVersion(ref string text) {
            var bytes = Convert.FromBase64String(text).ToList();
            int openIndex = bytes.FindIndex(x => x == (byte)openingChar);
            int closeIndex = bytes.FindIndex(x => x == (byte)closingChar) + 1; //plus one because the bytes are saved as [value], [extra 0] for each char

            if(openIndex == -1) {
                throw new ArgumentException("The cipher text doesn't have the opening bracket for the version.");
            }
            if(closeIndex == -1) {
                throw new ArgumentException("The cipher text doesn't have the closing bracket for the version.");
            }

            string version = Encoding.Unicode.GetString(bytes.Skip(openIndex).Take(closeIndex + 1).ToArray());
            version = version.TrimStart(openingChar).TrimEnd(closingChar);
            text = Convert.ToBase64String(bytes.Skip(openIndex + closeIndex + 1).ToArray());

            return version;
        }

    }

}
