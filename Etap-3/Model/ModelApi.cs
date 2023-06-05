using Data;
using Logic;
using System;
using System.Numerics;

namespace Model
{
    public class ModelApi
    {
        private LogicAbstractApi _logic;
        private Vector2 _screenSize;
        private Box _box;
        public event EventHandler<MovementEventArgs> BallMoved;
        private int _ballNumber;

        public ModelApi()
        {
            _screenSize = new Vector2(400, 400);
            _logic = new LogicApi(_screenSize);
            _box = _logic.GetBox();
            _logic.BallMoved += (who_send, argv) =>
            {
                var args = new MovementEventArgs(argv.Ball.Id, argv.Ball.Position, argv.Ball.R);
                BallMoved?.Invoke(this, args);
            };
        }

        public int GetBallsNumber()
        {
            return _ballNumber;
        }

        public void SetBallsNumber(int number)
        {
            _ballNumber = number;
        }

        public Vector2 GetScreenSize()
        {
            return _screenSize;
        }

        public void OnBallMoved(MovementEventArgs args)
        {
            BallMoved?.Invoke(this, args);
        }


        public void RunStartSimulation()
        {
            _logic.CreateBalls(_ballNumber, 15, 15, _box);
            _logic.StartSimulation(_box);
        }

        public void RunStopSimulation()
        {
            _logic.StopSimulation(_box);
        }

        public void ReRunSimulation()
        {
            _logic.ReRunSimulation(_box);
        }
    }
}
