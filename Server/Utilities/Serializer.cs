using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Server.Utilities
{
    public class Serializer : ISerializer
    {
        public string SerializeConnection(Connection connection)
        {
            var id = connection.Id;
            var circle = connection.Circle;

            return JsonSerializer.Serialize(new { id, circle});
        }
    }
}
