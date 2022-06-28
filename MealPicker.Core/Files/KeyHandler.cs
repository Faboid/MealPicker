using MealPicker.Core.Services;
using MealPicker.Encryption;
using MealPicker.Utils;
using System.Net;
using System.Security.Cryptography;

namespace MealPicker.Core.Files; 

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

    public KeyHandler(ICryptoService cryptoService) : this(cryptoService, PathBuilder.GetKeyPath) { }

    internal KeyHandler(ICryptoService cryptoService, string customPath) {
        this.cryptoService = cryptoService;
        path = customPath;
        cnnServiceFactory = new();
    }

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

    public enum KeyError {
        Undefined,
        Timeout,
        MissingKey,
        InvalidOrExpiredKey,
        WrongPassword,
    }

}
