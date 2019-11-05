using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class Client
    {
        public string Id { get; }
        public Circle Circle { get; }

        public Client(string id, Circle circle)
        {
            Id = id;
            Circle = circle;
        }
    }
}
