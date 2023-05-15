using System;
using System.Collections.Generic;
using System.Numerics;

namespace Data
{
    public class Movement
    {
        private Vector2 _screenSize;
        private readonly Random _random;
        private readonly IList<Ball> _ballsList;

        public Movement(Random random, Vector2 screenSize, IList<Ball> ballsList)
        {
            _random = random;
            _screenSize = screenSize;
            _ballsList = ballsList;
        }

        public Vector2 GenerateDirection()
        {
            Vector2 direction;
            direction.X = (float)(_random.NextDouble() * 10 - 5);
            direction.Y = (float)(_random.NextDouble() * 10 - 5);
            return direction;
        }

        public Vector2 SetStartPosition(float radius)
        {
            Vector2 point;
            point.X = _random.Next(Convert.ToInt32(_screenSize.X - radius * 2)) + radius;
            point.Y = _random.Next(Convert.ToInt32(_screenSize.Y - radius * 2)) + radius;
            return point;
        }

        public bool CheckPosition(Vector2 position, float radius)
        {
            foreach (var ball in _ballsList)
            {
                if (DoBallsCollide(position, radius, ball.Position, ball.R))
                {
                    return false;
                }
            }
            return true;
        }

        public bool DoBallsCollide(Vector2 pos1, float radius1, Vector2 pos2, float radius2)
        {
            var ballsDistance = (pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y);
            var ballsRadiusDistance = (radius1 / 2 + radius2 / 2) * (radius1 / 2 + radius2 / 2);
            return ballsDistance <= ballsRadiusDistance;
        }
    }
}
