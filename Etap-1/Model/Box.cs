using System;
using System.Collections.ObjectModel;

namespace Model
{
    public class Box
    {
        private readonly int _boxWidth;
        private readonly int _boxHeight;
        private readonly ObservableCollection<Ball> _ballsList;

        public Box()
        {
            _ballsList = new ObservableCollection<Ball>();
            _boxWidth = 400;
            _boxHeight = 400;
        }

        public void GenerateBalls(int number, int radius)
        {
            Random random = new Random();

            for (int i = 0; i < number; i++)
            {
                double x = random.Next(0, _boxWidth - radius);
                double y = random.Next(0, _boxHeight - radius);

                _ballsList.Add(new Ball(x, y, radius));
            }
        }

        public void MoveBalls()
        {
            while (true)
            {
                foreach (Ball ball in _ballsList)
                {
                    ball.MoveBall();
                }
            }
        }

        public int Width { get => _boxWidth; }
        public int Height { get => _boxHeight; }

        public ObservableCollection<Ball> Balls { get => _ballsList; }
    }
}
