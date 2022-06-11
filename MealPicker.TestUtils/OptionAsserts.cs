using MealPicker.Utils;
using MealPicker.Utils.Options;

namespace MealPicker.TestUtils;

public static class OptionAsserts {

    public static void Is<TSome>(this Option<TSome> option, OptionResult expected) {
        var actual = option.Result();
        Assert(expected, actual);
    }

    public static void Is<TSome, TError>(this Option<TSome, TError> option, OptionResult expected) {
        var actual = option.Result();
        Assert(expected, actual);
    }

    private static void Assert(OptionResult expected, OptionResult actual) {
        if(actual != expected) {
            throw new WrongResultAssertException($"Expected {expected} result, but it was {actual}.");
        }
    }

}

internal class WrongResultAssertException : Exception {
    public WrongResultAssertException(string? message) : base(message) { }
}