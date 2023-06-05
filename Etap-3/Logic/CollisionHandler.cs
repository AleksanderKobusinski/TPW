using Data;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Logic
{
    internal class CollisionHandler
    {
        public static Ball CheckBallsCollisions(Ball ballToCheck, IEnumerable<Ball> ballsList)
        {
            foreach (var ball in ballsList)
            {
                if (ReferenceEquals(ballToCheck, ball))
                {
                    continue;
                }

                if (DoBallsCollide(ballToCheck, ball))
                {
                    return ball;
                }
            }

            return null;
        }

        private static bool DoBallsCollide(Ball ball1, Ball ball2)
        {
            var ball1NextPos = ball1.Position + (Vector2.One * ball1.R / 2) + ball1.Direction;
            var ball2NextPos = ball2.Position + (Vector2.One * ball2.R / 2) + ball2.Direction;
            var ballsDistance = (ball1NextPos.X - ball2NextPos.X) * (ball1NextPos.X - ball2NextPos.X) + (ball1NextPos.Y - ball2NextPos.Y) * (ball1NextPos.Y - ball2NextPos.Y);
            var ballsRDistance = (ball1.R + ball2.R) * (ball1.R + ball2.R) / 4;

            return ballsDistance <= ballsRDistance;
        }

        public static void CollisionWithWall(Ball ball, Vector2 screenSize)
        {
            var ballNextPos = ball.Position + (Vector2.One * ball.R / 2) + ball.Direction;

            if (ballNextPos.X <= ball.R / 2 || ballNextPos.X + ball.R / 2 >= screenSize.X)
            {
                ball.Direction = new Vector2(-ball.Direction.X, ball.Direction.Y);
            }

            if (ballNextPos.Y <= ball.R / 2 || ballNextPos.Y + ball.R / 2 >= screenSize.Y)
            {
                ball.Direction = new Vector2(ball.Direction.X, -ball.Direction.Y);
            }
        }

        public static void CollisionWithBalls(Ball ball1, Ball ball2)
        {
            var ball1center = ball1.Position + (Vector2.One * ball1.R / 2);
            var ball2center = ball2.Position + (Vector2.One * ball2.R / 2);

            var centerDistancePow1 = Math.Pow((ball1center.X - ball2center.X), 2) + Math.Pow((ball1center.Y - ball2center.Y), 2);
            var massCalculation1 = (2 * ball2.Mass / (ball1.Mass + ball2.Mass));
            var dotProduct1 = Vector2.Dot((ball1.Direction - ball2.Direction), (ball1center - ball2center));
            var Oi1 = ball1center - ball2center;

            ball1.Direction -= massCalculation1 * dotProduct1 / (float)centerDistancePow1 * Oi1;

            var centerDistancePow2 = Math.Pow((ball2center.X - ball1center.X), 2) + Math.Pow((ball2center.Y - ball1center.Y), 2);
            var massCalculation2 = (2 * ball1.Mass / (ball1.Mass + ball2.Mass));
            var dotProduct2 = Vector2.Dot((ball2.Direction - ball1.Direction), (ball2center - ball1center));
            var Oi2 = ball2center - ball1center;

            ball2.Direction -= massCalculation2 * dotProduct2 / (float)centerDistancePow2 * Oi2;
        }
    }
}
