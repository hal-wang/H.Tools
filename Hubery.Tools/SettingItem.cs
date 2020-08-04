using SQLite;

namespace Hubery.Common.Base.Models
{
    public class SettingItem
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}