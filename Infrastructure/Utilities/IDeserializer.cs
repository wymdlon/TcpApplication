using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Utilities
{
    public interface IDeserializer
    {
        T Deserialize<T>(string stringValue);
    }
}
