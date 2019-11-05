using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class Activity
    {
        public enum Types
        {
            Up,
            Down,
            Right,
            Left
        }

        public Types Type { get; set; }
    }
}
