namespace UwpTest.Helpers {
    public class SettingHelper : HTools.Uwp.Helpers.SettingConfigBase {
        private SettingHelper() { }
        public static SettingHelper Instance { get; } = new SettingHelper();
    }
}
