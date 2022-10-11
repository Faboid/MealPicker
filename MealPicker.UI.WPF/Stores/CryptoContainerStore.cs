using MealPicker.Core.Files;
using MealPicker.Core.Services;
using System;
using System.Threading.Tasks;

namespace MealPicker.UI.WPF.Stores;

/// <summary>
/// Provides methods to set up and retrieve <see cref="IConnectionService"/>.
/// </summary>
public class CryptoContainerStore {

    private readonly IKeyHandlerFactory _keyHandlerFactory;

    public CryptoContainerStore(IKeyHandlerFactory keyHandlerFactory) {
        _keyHandlerFactory = keyHandlerFactory;
    }


    private IConnectionService? _connectionService;

    public async Task<SignupResult> SignupConnection(char[] password, string api_key) {

        var keyHandler = _keyHandlerFactory.CreateKeyHandler(password);
        var result = await keyHandler.TrySet(api_key);

        var conn = result.Or(null!);
        if(conn != null) {
            _connectionService = conn;
            return SignupResult.Success;
        }

        return result.OrError(0) switch {
            KeyHandler.KeyError.Timeout => SignupResult.Timeout,
            KeyHandler.KeyError.InvalidOrExpiredKey => SignupResult.InvalidKey,
            _ => SignupResult.Unknown,
        };

    }

    public async Task<LoginResult> LoginConnection(char[] password) {

        var keyHandler = _keyHandlerFactory.CreateKeyHandler(password);
        var result = await keyHandler.TryGet();

        var conn = result.Or(null!);
        if(conn != null) {
            _connectionService = conn;
            return LoginResult.Success;
        }

        return result.OrError(0) switch {
            KeyHandler.KeyError.Timeout => LoginResult.Timeout,
            KeyHandler.KeyError.InvalidOrExpiredKey => LoginResult.ExpiredKey,
            KeyHandler.KeyError.WrongPassword => LoginResult.WrongPassword,
            _ => LoginResult.Unknown,
        };

    }

    public IConnectionService RetrieveConnection() => _connectionService ?? throw new InvalidOperationException("Attempted to access the stored IConnectionService before it's set.");

    public enum SignupResult {
        Unknown,
        Success,
        InvalidKey,
        Timeout,
    }

    public enum LoginResult {
        Unknown,
        Success,
        Timeout,
        ExpiredKey,
        WrongPassword,
    }

}