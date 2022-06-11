using MealPicker.Core.Services;
using MealPicker.Encryption;
using MealPicker.Utils;
using System.Net;
using System.Security.Cryptography;

namespace MealPicker.Core.Files; 

public class KeyHandler {

    readonly ICryptoService cryptoService;
    readonly string path;

    public KeyHandler(ICryptoService cryptoService) : this(cryptoService, PathBuilder.GetKeyPath) { }

    internal KeyHandler(ICryptoService cryptoService, string customPath) {
        this.cryptoService = cryptoService;
        path = customPath;
    }

    public async Task<Option<ConnectionService, KeyError>> TrySet(string key) {
        var connection = await ConnectionService.CreateConnectionAsync(new (key));
        var output = connection.Match<Option<ConnectionService, KeyError>>(
            some => throw new Exception(),
            error => ConvertHttpStatusToKeyError(error.StatusCode),
            () => KeyError.Undefined
        );

        if(output.Result() == Utils.Options.OptionResult.Some) {
            string encrypted = cryptoService.Encrypt(key);
            await File.WriteAllTextAsync(path, encrypted);
        }

        return output;
    }

    public async Task<Option<ConnectionService, KeyError>> TryGet() {
        if(File.Exists(path) == false) {
            return KeyError.MissingKey;
        }

        string cipher = await File.ReadAllTextAsync(path).ConfigureAwait(false);
        if(cipher == null || cipher.Length == 0) {
            return KeyError.MissingKey;
        }

        try {
            var key = cryptoService.Decrypt(cipher);
            var connectionOption = await ConnectionService.CreateConnectionAsync(new(key)).ConfigureAwait(false);

            return connectionOption.Match<Option<ConnectionService, KeyError>>(
                    some => some,
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

    public enum KeyError {
        Undefined,
        Timeout,
        MissingKey,
        InvalidOrExpiredKey,
        WrongPassword,
    }

}
