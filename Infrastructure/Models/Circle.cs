using System;

namespace Infrastructure.Models
{
    public class Circle
    {
        public const int MinXBound = 0;
        public const int MaxXBound = 50;

        public const int MinYBound = 0;
        public const int MaxYBound = 20;

        public int Radius { get; private set; }
        public int X { get; protected internal set; }
        public int Y { get; protected internal set; }

        public Circle()
        {
            Random random = new Random();
            X = random.Next(MinXBound, MaxXBound);
            Y = random.Next(MinYBound, MaxYBound);
            Radius = 1;
        }
    }
}
