using Newtonsoft.Json;

namespace Infrastructure.Utilities
{
    public class Serializer : ISerializer
    {
        public string Serialize<T>(T item)
        {
            return JsonConvert.SerializeObject(
                item,
                new Newtonsoft.Json.Converters.StringEnumConverter()
            );
        }
    }
}
