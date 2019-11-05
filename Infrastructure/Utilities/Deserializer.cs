using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Utilities
{
    public class Deserializer : IDeserializer
    {
        public T Deserialize<T>(string stringValue)
        {
            return JsonConvert.DeserializeObject<T>(
                stringValue,
                new Newtonsoft.Json.Converters.StringEnumConverter()
            );
        }
    }
}
