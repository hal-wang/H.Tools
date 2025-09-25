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

        Assert.IsNull(val);
    }

    [TestMethod]
    public void GetDefault()
    {
        var config = new AppSettingsConfiguration();
        config.Remove("DefaultValue");
        var val = config.Get("DefaultValue");

        Assert.AreEqual("DefaultValue", val);
    }

    [TestMethod]
    public void FindWithCache()
    {
        var config1 = new AppSettingsConfiguration();
        config1.Set(1, nameof(FindWithCache));
        var val1 = config1.Find<int>(nameof(FindWithCache));
        Assert.AreEqual(1, val1);

        var config2 = new AppSettingsConfiguration();
        config2.Set(2, nameof(FindWithCache));
        var val2 = config2.Find<int>(nameof(FindWithCache));
        Assert.AreEqual(2, val2);

        var val31 = config1.Find<int>(nameof(FindWithCache));
        var val32 = config1.Get(0, nameof(FindWithCache));
        Assert.AreEqual(1, val31);
        Assert.AreEqual(1, val32);
    }

    [TestMethod]
    public void FindWithoutCache()
    {
        var config1 = new AppSettingsConfiguration();
        config1.Set(1, nameof(FindWithoutCache));
        var val1 = config1.Find<int>(nameof(FindWithoutCache));
        Assert.AreEqual(1, val1);

        var config2 = new AppSettingsConfiguration();
        config2.Set(2, nameof(FindWithoutCache));
        var val2 = config2.Find<int>(nameof(FindWithoutCache));
        Assert.AreEqual(2, val2);

        var val31 = config1.Find<int>(true, nameof(FindWithoutCache));
        var val32 = config1.Get(true, 0, nameof(FindWithoutCache));
        Assert.AreEqual(2, val31);
        Assert.AreEqual(2, val32);
    }

    [TestMethod]
    public void Remove()
    {
        var config1 = new AppSettingsConfiguration();
        config1.Set(1, nameof(Remove));
        var val1 = config1.Find<int>(nameof(Remove));
        Assert.AreEqual(1, val1);

        var config2 = new AppSettingsConfiguration();
        config2.Set(2, nameof(Remove));
        var val2 = config2.Find<int>(nameof(Remove));
        Assert.AreEqual(2, val2);

        config2.Remove(nameof(Remove));

        var val31 = config1.Find<int>(nameof(Remove));
        var val32 = config1.Get(-1, nameof(Remove));
        Assert.AreEqual(1, val31);
        Assert.AreEqual(1, val32);

        var val41 = config1.Find<int>(true, nameof(Remove));
        var val42 = config1.Get(true, -1, nameof(Remove));
        Assert.AreEqual(0, val41);
        Assert.AreEqual(-1, val42);
    }

    [TestMethod]
    public void GetWithCache()
    {
        var config1 = new AppSettingsConfiguration();
        config1.Remove(nameof(GetWithCache));
        var val1 = config1.Get(1, nameof(GetWithCache));
        Assert.AreEqual(1, val1);

        var config2 = new AppSettingsConfiguration();
        var val2 = config2.Get(2, nameof(GetWithCache));
        var val3 = config2.Get(2, nameof(GetWithCache));
        Assert.AreEqual(1, val2);
        Assert.AreEqual(1, val3);
    }
}