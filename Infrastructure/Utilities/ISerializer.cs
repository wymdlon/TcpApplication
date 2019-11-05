using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Utilities
{
    public interface ISerializer
    {
        string Serialize<T>(T item);
    }
}
