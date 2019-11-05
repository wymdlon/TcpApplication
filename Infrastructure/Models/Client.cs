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

        public void Move(Activity activity)
        {
            switch (activity.Type)
            {
                case Activity.Types.Up:
                    MoveUp();
                    break;
                case Activity.Types.Down:
                    MoveDown();
                    break;
                case Activity.Types.Right:
                    MoveRight();
                    break;
                case Activity.Types.Left:
                    MoveLeft();
                    break;

                default:
                    break;
            }
        }

        private void MoveUp()
        {
            if (Circle.Y < Circle.MaxYBound)
                Circle.Y++;
        }

        private void MoveDown()
        {
            if (Circle.Y > Circle.MinYBound)
                Circle.Y--;
        }

        private void MoveRight()
        {
            if (Circle.X < Circle.MaxXBound)
                Circle.X++;
        }

        private void MoveLeft()
        {
            if (Circle.X > Circle.MinXBound)
                Circle.X--;
        }
    }
}
