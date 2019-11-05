using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Utilities
{
    public interface ISerializer
    {
        string SerializeConnection(Connection connection);
    }
}
