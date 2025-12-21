using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

namespace H.Tools.Config;

public abstract class Configuration : IConfiguration
{
    protected abstract string GetValue(string key);
    protected abstract void SetValue(string value, string key);
    protected abstract void RemoveKey(string key);

    public abstract bool ContainsKey(string key);

    private readonly Dictionary<string, object> _cache = [];

    public virtual T Find<T>(bool force, [CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

        if (!force && _cache.TryGetValue(key, out var obj))
        {
            return (T)obj;
        }

        if (ContainsKey(key))
        {
            var val = (T)Convert.ChangeType(GetValue(key), typeof(T));
            _cache[key] = val;
            return val;
        }
        else
        {
            return default;
        }
    }

    public virtual T Find<T>([CallerMemberName] string key = null)
    {
        return Find<T>(false, key);
    }

    public virtual T Get<T>(bool force, T defaultValue = default, [CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

        if (!force && _cache.TryGetValue(key, out var obj))
        {
            return (T)obj;
        }

        if (!ContainsKey(key))
        {
            Set(defaultValue, key);
            _cache[key] = defaultValue;
            return defaultValue;
        }
        else
        {
            var val = (T)Convert.ChangeType(GetValue(key), typeof(T));
            _cache[key] = val;
            return val;
        }
    }

    public virtual T Get<T>(T defaultValue = default, [CallerMemberName] string key = null)
    {
        return Get(false, defaultValue, key);
    }

    public virtual void Set<T>(T value, [CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

        _cache[key] = value;
        var val = value?.ToString() ?? "";
        SetValue(val, key);
    }

    public virtual void Remove(string key)
    {
        RemoveKey(key);
        _cache.Remove(key);
    }

    public virtual string this[string key]
    {
        get => Get<string>(null, key);
        set => Set(value, key);
    }
}
