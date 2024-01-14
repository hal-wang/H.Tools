using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace H.Tools.Config;

public abstract class ConfigBase<U>
{
    protected abstract U GetValue(string key);
    protected abstract void SetValue(U value, string key);

    public abstract bool ContainsKey(string key);

    public abstract void Remove(string key);

    public T Find<T>([CallerMemberName] string key = null)
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

    public T Get<T>(T defaultValue = default, [CallerMemberName] string key = null)
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

    public void Set<T>(T value, [CallerMemberName] string key = null)
    {
        if (string.IsNullOrEmpty(key)) throw new NoNullAllowedException();

        SetValue((U)(object)value, key);
    }

    public string this[string key]
    {
        get => Get<string>(null, key);
        set => Set(value, key);
    }
}
