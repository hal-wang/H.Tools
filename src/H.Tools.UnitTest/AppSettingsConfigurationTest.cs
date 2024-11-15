using H.Tools.Config;

namespace H.Tools.UnitTest;

[TestClass]
public class AppSettingsConfigurationTest
{
    [TestMethod]
    public void FindAndNotExist()
    {
        var config = new AppSettingsConfiguration();
        var val = config.Find<string>("NotExist");

        Assert.AreEqual(val, null);
    }

    [TestMethod]
    public void GetDefault()
    {
        var config = new AppSettingsConfiguration();
        config.Remove("DefaultValue");
        var val = config.Get("DefaultValue");

        Assert.AreEqual(val, "DefaultValue");
    }

    [TestMethod]
    public void FindWithCache()
    {
        var guid1 = Guid.NewGuid().ToString();
        var config1 = new AppSettingsConfiguration();
        config1.Set(guid1, nameof(FindWithCache));
        var val1 = config1.Find<string>(nameof(FindWithCache));
        Assert.AreEqual(val1, guid1);

        var guid2 = Guid.NewGuid().ToString();
        var config2 = new AppSettingsConfiguration();
        config2.Set(guid2, nameof(FindWithCache));
        var val2 = config2.Find<string>(nameof(FindWithCache));
        Assert.AreEqual(val2, guid2);

        var val31 = config1.Find<string>(nameof(FindWithCache));
        var val32 = config1.Get("", nameof(FindWithCache));
        Assert.AreEqual(val31, guid1);
        Assert.AreEqual(val32, guid1);
    }

    [TestMethod]
    public void FindWithoutCache()
    {
        var guid1 = Guid.NewGuid().ToString();
        var config1 = new AppSettingsConfiguration();
        config1.Set(guid1, nameof(FindWithoutCache));
        var val1 = config1.Find<string>(nameof(FindWithoutCache));
        Assert.AreEqual(val1, guid1);

        var guid2 = Guid.NewGuid().ToString();
        var config2 = new AppSettingsConfiguration();
        config2.Set(guid2, nameof(FindWithoutCache));
        var val2 = config2.Find<string>(nameof(FindWithoutCache));
        Assert.AreEqual(val2, guid2);

        var val31 = config1.Find<string>(true, nameof(FindWithoutCache));
        var val32 = config1.Get(true, "", nameof(FindWithoutCache));
        Assert.AreEqual(val31, guid2);
        Assert.AreEqual(val32, guid2);
    }
}