using Data;
using System;
using System.Numerics;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public event EventHandler<BallEventArgs> BallMoved;

        public virtual void WhenBallMoved(BallEventArgs args)
        {
            BallMoved?.Invoke(this, args);
        }

        public abstract void CreateBalls(int count, int radius, int mass, Box box);
        public abstract Box GetBox();
        public abstract void StartSimulation(Box box);
        public abstract void StopSimulation(Box box);
        public abstract void ReRunSimulation(Box box);
    }

    public class LogicApi : LogicAbstractApi
    {

        private Box box;
        private readonly object _locker = new object();
        private Vector2 _screenSize;

        public LogicApi(Vector2 screenSize)
        {
            _screenSize = screenSize;
            box = new Box(_screenSize);
        }

        public override void CreateBalls(int count, int radius, int mass, Box box)
        {
            box.GenerateBalls(count, radius, mass);
        }

        public override Box GetBox()
        {
            return box;
        }

        private void WhenBallMovedAndCollide(object _, BallsEventArgs args)
        {
            this.WhenBallMoved(new BallEventArgs(args.Ball));

            lock (_locker)
            {
                var collidedBall = CollisionHandler.CheckBallsCollisions(args.Ball, args.Balls);
                if (collidedBall != null)
                {
                    CollisionHandler.CollisionWithBalls(args.Ball, collidedBall);
                }
            }

            CollisionHandler.CollisionWithWall(args.Ball, box.ScreenSize);
        }

        public override void StartSimulation(Box box)
        {
            box.BallMoved += WhenBallMovedAndCollide;
            box.MoveBalls();
        }

        public override void StopSimulation(Box box)
        {
            box.StopBalls();
        }

        public override void ReRunSimulation(Box box)
        {
            box.ReRunBalls();
            box.MoveBalls();
        }
    }
}
