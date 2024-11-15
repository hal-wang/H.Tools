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
        var config1 = new AppSettingsConfiguration();
        config1.Set(1, nameof(FindWithCache));
        var val1 = config1.Find<int>(nameof(FindWithCache));
        Assert.AreEqual(val1, 1);

        var config2 = new AppSettingsConfiguration();
        config2.Set(2, nameof(FindWithCache));
        var val2 = config2.Find<int>(nameof(FindWithCache));
        Assert.AreEqual(val2, 2);

        var val31 = config1.Find<int>(nameof(FindWithCache));
        var val32 = config1.Get(0, nameof(FindWithCache));
        Assert.AreEqual(val31, 1);
        Assert.AreEqual(val32, 1);
    }

    [TestMethod]
    public void FindWithoutCache()
    {
        var config1 = new AppSettingsConfiguration();
        config1.Set(1, nameof(FindWithoutCache));
        var val1 = config1.Find<int>(nameof(FindWithoutCache));
        Assert.AreEqual(val1, 1);

        var config2 = new AppSettingsConfiguration();
        config2.Set(2, nameof(FindWithoutCache));
        var val2 = config2.Find<int>(nameof(FindWithoutCache));
        Assert.AreEqual(val2, 2);

        var val31 = config1.Find<int>(true, nameof(FindWithoutCache));
        var val32 = config1.Get(true, 0, nameof(FindWithoutCache));
        Assert.AreEqual(val31, 2);
        Assert.AreEqual(val32, 2);
    }

    [TestMethod]
    public void Remove()
    {
        var config1 = new AppSettingsConfiguration();
        config1.Set(1, nameof(Remove));
        var val1 = config1.Find<int>(nameof(Remove));
        Assert.AreEqual(val1, 1);

        var config2 = new AppSettingsConfiguration();
        config2.Set(2, nameof(Remove));
        var val2 = config2.Find<int>(nameof(Remove));
        Assert.AreEqual(val2, 2);

        config2.Remove(nameof(Remove));

        var val31 = config1.Find<int>(nameof(Remove));
        var val32 = config1.Get(-1, nameof(Remove));
        Assert.AreEqual(val31, 1);
        Assert.AreEqual(val32, 1);

        var val41 = config1.Find<int>(true, nameof(Remove));
        var val42 = config1.Get(true, -1, nameof(Remove));
        Assert.AreEqual(val41, 0);
        Assert.AreEqual(val42, -1);
    }
}