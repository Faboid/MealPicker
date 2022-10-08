using MealPicker.Core.Services;
using MealPicker.Encryption;
using MealPicker.Utils;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Cryptography;

namespace MealPicker.Core.Files;

/// <summary>
/// Handles storage, encryption, and retrieval of the API key.
/// </summary>
public class KeyHandler : IDisposable {

    /// <summary>
    /// Checks if the default file exists and has a string that resembles a key.
    /// <br/>Note: this returns <see langword="true"/> if the key might exist. It doesn't necessarily mean it's a valid key.
    /// </summary>
    /// <returns></returns>
    public static bool KeyMightExist() {
        var path = PathBuilder.GetKeyPath;
        if(!File.Exists(path)) {
            return false;
        }
        
        if(string.IsNullOrWhiteSpace(File.ReadAllText(path))) {
            return false;
        }

        return true;
    }

    private bool isDisposed = false;
    readonly ICryptoService cryptoService;
    readonly string path;

    private readonly ConnectionServiceFactory cnnServiceFactory;

    /// <summary>
    /// Initializes an instance of <see cref="KeyHandler"/> with the default path.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="cryptoService"></param>
    public KeyHandler(ICryptoService cryptoService, ILoggerFactory? loggerFactory = null) : this(cryptoService, PathBuilder.GetKeyPath, loggerFactory) { }

    /// <summary>
    /// Initializes an instance of <see cref="KeyHandler"/> with a custom path.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="cryptoService"></param>
    /// <param name="customPath"></param>
    internal KeyHandler(ICryptoService cryptoService, string customPath, ILoggerFactory? loggerFactory = null) {
        this.cryptoService = cryptoService;
        path = customPath;
        cnnServiceFactory = new(loggerFactory);
    }

    /// <summary>
    /// Checks the validity of the key, then encrypts it and stores it if it's correct.
    /// </summary>
    /// <param name="key">The API key that will be saved.</param>
    /// <returns>An option with either a <see cref="IConnectionService"/> if successful or a <see cref="KeyError"/> on failure.</returns>
    public async Task<Option<IConnectionService, KeyError>> TrySet(string key) {
        var connection = await cnnServiceFactory.BuildConnectionService(new (key));
        var output = connection.Match(
            some => Option.Some<IConnectionService, KeyError>(some),
            error => ConvertHttpStatusToKeyError(error.StatusCode),
            () => KeyError.Undefined
        );

        if(output.Result() == Utils.Options.OptionResult.Some) {
            string encrypted = cryptoService.Encrypt(key);
            await File.WriteAllTextAsync(path, encrypted);
        }

        return output;
    }

    /// <summary>
    /// Attempts retrieving and decrypting the API key.
    /// </summary>
    /// <returns>An option with <see cref="IConnectionService"/> on success, and <see cref="KeyError"/> on failure.</returns>
    public async Task<Option<IConnectionService, KeyError>> TryGet() {
        if(File.Exists(path) == false) {
            return KeyError.MissingKey;
        }

        string cipher = await File.ReadAllTextAsync(path).ConfigureAwait(false);
        if(cipher == null || cipher.Length == 0) {
            return KeyError.MissingKey;
        }

        try {
            var key = cryptoService.Decrypt(cipher);
            var connectionOption = await cnnServiceFactory.BuildConnectionService(new(key)).ConfigureAwait(false);

            return connectionOption.Match(
                    some => Option.Some<IConnectionService, KeyError>(some),
                    error => ConvertHttpStatusToKeyError(error.StatusCode),
                    () => KeyError.Undefined
                );

        } catch (CryptographicException) {
            return KeyError.WrongPassword;
        }
    }

    private static KeyError ConvertHttpStatusToKeyError(HttpStatusCode status) => status switch {
        HttpStatusCode.Unauthorized => KeyError.InvalidOrExpiredKey,
        HttpStatusCode.GatewayTimeout => KeyError.Timeout,
        HttpStatusCode.NetworkAuthenticationRequired => KeyError.InvalidOrExpiredKey,
        _ => KeyError.Undefined,
    };

    public void Dispose() {
        lock(cryptoService) {
            if(isDisposed) {
                return;
            }

            cryptoService.Dispose();
            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// A list of errors that may happen when storing or retrieving the key.
    /// </summary>
    public enum KeyError {
        Undefined,
        Timeout,
        MissingKey,
        InvalidOrExpiredKey,
        WrongPassword,
    }

}
