﻿namespace MealPicker.Utils.Options;

public struct None<TValue> : IOption<TValue> {

    public None() { }

    public OptionResult Result() => OptionResult.None;
    public T Match<T>(Func<TValue, T> some, Func<T> none) => none.Invoke();
    public Option<T> Bind<T>(Func<TValue, Option<T>> func) => new None<T>();
    public Task<Option<T>> BindAsync<T>(Func<TValue, Task<Option<T>>> func) => Task.FromResult<Option<T>>(new None<T>());
    public TValue Or(TValue def) => def;

}

public struct None<TValue, TError> : IOption<TValue, TError> {

    public None() { }

    public OptionResult Result() => OptionResult.None;
    public T Match<T>(Func<TValue, T> some, Func<TError, T> error, Func<T> none) => none.Invoke();
    public Option<T, TError> Bind<T>(Func<TValue, Option<T, TError>> func) => new None<T, TError>();
    public Task<Option<T, TError>> BindAsync<T>(Func<TValue, Task<Option<T, TError>>> func) => Task.FromResult<Option<T, TError>>(new None<T, TError>());
    public TValue Or(TValue def) => def;
    public TError OrError(TError def) => def;

}
