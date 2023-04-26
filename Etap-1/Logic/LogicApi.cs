using Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    public class LogicApi
    {
        private readonly CancellationToken _cancelToken;
        private readonly List<Task> _tasksList = new List<Task>();

        public void CreateBalls(int count, int radius, Box box)
        {
            box.GenerateBalls(count, radius);
        }

        public Box CreateBox()
        {
            return new Box();
        }

        public void StartSimulation(Box box)
        {
            foreach (Ball ball in box.Balls)
            {
                Task task = Task.Run(async () =>
                {
                    await Task.Delay(1);

                    while (ball.Alive)
                    {
                        await Task.Delay(20);
                        try
                        {
                            _cancelToken.ThrowIfCancellationRequested();
                        }
                        catch (OperationCanceledException)
                        {
                            break;
                        }

                        ball.MoveBall();

                    }
                });

                _tasksList.Add(task);
            }
        }

        public void StopSimulation(Box box)
        {
            foreach (Ball ball in box.Balls)
            {
                ball.Alive = false;
            }

            box.Balls.Clear();
            _tasksList.Clear();
        }
    }
}
