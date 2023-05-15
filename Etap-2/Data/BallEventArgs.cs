using System;
using System.Collections.Generic;

namespace Data
{
    public class BallEventArgs : EventArgs
    {
        public readonly Ball Ball;
        public BallEventArgs(Ball ball)
        {
            Ball = ball;
        }
    }

    public class BallsEventArgs : EventArgs
    {
        public readonly Ball Ball;
        public readonly IList<Ball> Balls;
        public BallsEventArgs(Ball ball, IList<Ball> balls)
        {
            Ball = ball;
            Balls = balls;
        }
    }
}
