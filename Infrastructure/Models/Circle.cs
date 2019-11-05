using System;

namespace Infrastructure.Models
{
    public class Circle
    {
        public int Radius { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        public Circle()
        {
            Random random = new Random();
            X = random.Next(0, 100);
            Y = random.Next(0, 100);
            Radius = 1;
        }
    }
}
