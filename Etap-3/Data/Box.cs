using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;

namespace Data
{
    public class Box
    {
        public Vector2 ScreenSize { get; set; }
        public event EventHandler<BallsEventArgs> BallMoved;

        private IList<Ball> _ballsList;
        private Random _random;
        private readonly BallsMovementLogs _logger;

        public void Init(Vector2 screenSize)
        {
            ScreenSize = screenSize;
            _ballsList = new List<Ball>();
            _random = new Random();
        }

        public Box(Vector2 screenSize)
        {
            Init(screenSize);
            _logger = new BallsMovementLogs("");
        }

        public Box(Vector2 screenSize, string logsFileName)
        {
            Init(screenSize);
            _logger = new BallsMovementLogs(logsFileName);
        }

        public virtual void WhenBallMoved(BallsEventArgs args)
        {
            _logger.LogBallsMovement(args.Ball);
            BallMoved?.Invoke(this, args);
        }

        public void GenerateBalls(int number, int radius, int mass)
        {
            _ballsList = new List<Ball>();

            Movement movement = new Movement(_random, ScreenSize, _ballsList);
            Stopwatch timer = new Stopwatch();

            for (int i = 0; i < number; i++)
            {
                var isPositionFree = false;
                var position = new Vector2(0, 0);

                while (!isPositionFree)
                {
                    position = movement.SetStartPosition(radius);
                    isPositionFree = movement.CheckPosition(position, radius);
                }

                Ball ball = new Ball(_ballsList.Count, position, radius, mass, movement.GenerateDirection(), true);
                _ballsList.Add(ball);
            }
        }

        public IList<Ball> GetBalls()
        {
            return _ballsList;
        }

        public void StopBalls()
        {
            foreach (var ball in _ballsList)
            {
                ball.IsAlive = false;
            }
        }

        public void ReRunBalls()
        {
            foreach (var ball in _ballsList)
            {
                ball.IsAlive = true;
            }
        }

        public void MoveBalls()
        {
            foreach (var ball in _ballsList)
            {
                ball.Moved += (sender, argv) =>
                {
                    var args = new BallsEventArgs(argv.Ball, new List<Ball>(_ballsList));
                    WhenBallMoved(args);
                };
                Task.Factory.StartNew(ball.MoveBall);
            }
        }
    }
}
