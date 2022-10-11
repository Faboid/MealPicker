using MealPicker.Encryption;
using Microsoft.Extensions.Logging;

namespace MealPicker.Core.Files;

public class KeyHandlerFactory : IKeyHandlerFactory {

    private readonly ILoggerFactory? _loggerFactory;
    private readonly ICryptoServiceFactory _cryptoServiceFactory;

    public KeyHandlerFactory(ICryptoServiceFactory cryptoServiceFactory, ILoggerFactory? loggerFactory = null) {
        _loggerFactory = loggerFactory;
        _cryptoServiceFactory = cryptoServiceFactory;
    }

    public IKeyHandler CreateKeyHandler(char[] password) {
        var crypto = _cryptoServiceFactory.CreateCryptoService(password);
        return new KeyHandler(crypto, _loggerFactory);
    }

}