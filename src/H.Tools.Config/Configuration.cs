using System;
using System.Collections.Concurrent;
using System.Data;
using System.Runtime.CompilerServices;

namespace H.Tools.Config;

public abstract class Configuration : IConfiguration
{
    protected abstract string GetValue(string key);
    protected abstract void SetValue(string value, string key);
    protected abstract void RemoveKey(string key);

    public abstract bool ContainsKey(string key);

    private readonly ConcurrentDictionary<string, object> _cache = [];

    public virtual T Find<T>(bool force, [CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

        if (!force && _cache.TryGetValue(key, out var cached))
        {
            return (T)cached;
        }

        if (!ContainsKey(key))
        {
            return default;
        }

        var typed = (T)Convert.ChangeType(GetValue(key), typeof(T));
        _cache.TryAdd(key, typed);
        return typed;
    }

    public virtual T Find<T>([CallerMemberName] string key = null)
    {
        return Find<T>(false, key);
    }

    public virtual T Get<T>(bool force, T defaultValue = default, [CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new ArgumentNullException();

        if (!force && _cache.TryGetValue(key, out var cached))
        {
            return (T)cached;
        }

        if (!ContainsKey(key))
        {
            Set(defaultValue, key);
            return defaultValue;
        }

        var typed = (T)Convert.ChangeType(GetValue(key), typeof(T));
        _cache.TryAdd(key, typed);
        return typed;
    }

    public virtual T Get<T>(T defaultValue = default, [CallerMemberName] string key = null)
    {
        return Get(false, defaultValue, key);
    }

    public virtual void Set<T>(T value, [CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

        _cache.AddOrUpdate(key, value, (_, _) => value);
        SetValue(value?.ToString(), key);
    }

    public virtual void Remove(string key)
    {
        RemoveKey(key);
        _cache.TryRemove(key, out _);
    }

    public virtual string this[string key]
    {
        get => Get<string>(null, key);
        set => Set(value, key);
    }
}
