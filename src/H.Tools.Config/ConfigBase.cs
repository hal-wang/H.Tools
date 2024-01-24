using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace H.Tools.Config;

public abstract class ConfigBase
{
    protected abstract string GetValue(string key);
    protected abstract void SetValue(string value, string key);

    public abstract bool ContainsKey(string key);

    public abstract void Remove(string key);

    public virtual T Find<T>([CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

        if (!ContainsKey(key))
        {
            return default;
        }
        else
        {
            return (T)Convert.ChangeType(GetValue(key), typeof(T));
        }
    }

    public virtual T Get<T>(T defaultValue = default, [CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

        if (!ContainsKey(key))
        {
            return defaultValue;
        }
        else
        {
            return (T)Convert.ChangeType(GetValue(key), typeof(T));
        }
    }

    public virtual void Set<T>(T value, [CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

        SetValue(value?.ToString() ?? "", key);
    }

    public virtual string this[string key]
    {
        get => Get<string>(null, key);
        set => Set(value, key);
    }
}
