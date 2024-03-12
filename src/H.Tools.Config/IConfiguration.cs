using System.Runtime.CompilerServices;

namespace H.Tools.Config;

public interface IConfiguration
{
    bool ContainsKey(string key);
    void Remove(string key);
    T Find<T>([CallerMemberName] string key = null);
    T Get<T>(T defaultValue = default, [CallerMemberName] string key = null);
    void Set<T>(T value, [CallerMemberName] string key = null);
    public string this[string key] { get; set; }
}
