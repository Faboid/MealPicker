using System.Runtime.InteropServices;
using System.Text;

namespace MealPicker.Encryption.Cryptography {

    /// <summary>
    /// Represents a password key.
    /// </summary>
    public class Key : IDisposable {

        /// <summary>
        /// Creates an instance of <see cref="Key"/> using the given byte array. The byte array will be copied and then set to 0.
        /// </summary>
        /// <param name="password"></param>
        public Key(byte[] password) : this(Encoding.Unicode.GetChars(password)) {
            for(int i = 0; i < password.Length; i++) {
                password[i] = 0;
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="Key"/> using the given char array. The chair array will be copied and then set to 0.
        /// </summary>
        /// <param name="password"></param>
        public Key(char[] password) {
            key = new char[password.Length];
            gcHandle = GCHandle.Alloc(key, GCHandleType.Pinned);
            password.CopyTo(key, 0);
            Clear(password);
        }

        private readonly char[] key;
        private GCHandle gcHandle;

        /// <summary>
        /// Gets the key. To ensure proper cleanup, it's suggested to not change/copy any element.
        /// </summary>
        /// <returns></returns>
        public char[] Get() {
            return key;
        }

        private static void Clear(char[] arr) {
            for(int i = 0; i < arr.Length; i++) {
                arr[i] = default;
            }
        }

        public void Dispose() {
            Clear(key);
            gcHandle.Free();
            GC.SuppressFinalize(this);
        }
    }
}
