using Newtonsoft.Json;
using System.Dynamic;

namespace HTools
{
    public static class DynamicHelper
    {
        public static dynamic ToExpandoObject(this object obj)
        {
            return JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(obj));
        }
    }
}
